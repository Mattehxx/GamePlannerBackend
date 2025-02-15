﻿using GamePlanner.DAL.Data.Auth;
using GamePlanner.DTO.Mapper;
using GamePlanner.DTO.OutputDTO.GeneralDTO;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,
        IMapper mapper, IBlobService blobService) : ODataController
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IBlobService _blobService = blobService;

        #region CRUD
        [Authorize]
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<ApplicationUser> options)
        {
            try
            {
                return options is null
                    ? BadRequest()
                    : Ok(_unitOfWork.ApplicationUserManager.Get(options));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usersDTO = new List<ApplicationUserOutputDTO>();

                var users = await _unitOfWork.ApplicationUserManager.GetAll().Where(u => !u.IsDeleted).OrderBy(u => u.Name).ToListAsync();

                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(r => r.Equals(UserRoles.Admin)) ?? UserRoles.User;
                    usersDTO.Add(new ApplicationUserOutputDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        BirthDate = user.BirthDate,
                        Level = user.Level,
                        IsDisabled = user.IsDisabled,
                        PhoneNumber = user.PhoneNumber,
                        IsDeleted = user.IsDeleted,
                        UserName = user.UserName,
                        Email = user.Email,
                        ImgUrl = user.ImgUrl,
                        Role = role
                    });
                }

                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return id == string.Empty
                    ? BadRequest("Invalid user id")
                    : Ok(await _unitOfWork.ApplicationUserManager.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
       
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] JsonPatchDocument<ApplicationUser> jsonPatch)
        {
            try
            {
                return jsonPatch == null
                    ? BadRequest("Invalid user")
                    : Ok(await _unitOfWork.ApplicationUserManager.PatchAsync(id, jsonPatch));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
        [Authorize]
        [HttpPut("image/{id}")]
        public async Task<IActionResult> UpdateImage(string id, IFormFile file)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUserManager.GetAll().SingleOrDefaultAsync(u => u.Id == id);
                if (user == null) return BadRequest("User not found");

                var containerClient = _blobService.GetBlobContainerClient("user-container");
                var imageUrl = await _blobService.UploadFileAsync(containerClient, file);
                user.ImgUrl = imageUrl;

                return Ok(await _unitOfWork.ApplicationUserManager.UpdateAsync(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("toggle-visibility/{id}")]
        public async Task<IActionResult> DisableOrEnable(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || Guid.TryParse(id, out Guid res))
                    return BadRequest("invalid id format");
                return Ok(await _unitOfWork.ApplicationUserManager.DisableOrEnableUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("download-apk")]
        public IActionResult DownloadApk()
        {
            try
            {
                return Ok("https://github.com/GabryTm047/download-APK-PW1-2anno-_ITS/raw/refs/heads/main/GamePlanner.apk");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}