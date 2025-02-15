﻿using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IUnitOfWork unitOfWork, IMapper mapper, IBlobService blobService) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IBlobService _blobService = blobService;

        #region CRUD

        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Game> options)
        {
            try
            {
                if (options == null) return BadRequest("No options found");
                return Ok(_unitOfWork.GameManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GameInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.CreateAsync(_mapper.ToEntity(model)));
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.DeleteAsync(id));
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Game> jsonPatch)
        {
            try
            {
                if (jsonPatch == null) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.PatchAsync(id, jsonPatch));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("image/{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            try
            {
                var game = await _unitOfWork.GameManager.GetByIdAsync(id);
                if (game == null) return BadRequest("User not found");

                var containerClient = _blobService.GetBlobContainerClient("game-container");
                var imageUrl = await _blobService.UploadFileAsync(containerClient, file);
                game.ImgUrl = imageUrl;

                return Ok(await _unitOfWork.GameManager.UpdateAsync(game));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("DisableOrEnable/{id}")]
        public async Task<IActionResult> DisableOrEnable(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.DisableOrEnableGame(id));
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
