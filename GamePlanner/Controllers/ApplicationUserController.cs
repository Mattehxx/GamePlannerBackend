using GamePlanner.DAL.Data.Auth;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,
        IMapper mapper) : ODataController
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #region CRUD

        [HttpGet]
        public IActionResult Get(ODataQueryOptions<ApplicationUser> options)
        {
            try
            {
                return options == null ? BadRequest("No options found") : Ok(_unitOfWork.ApplicationUserManager.Get(options));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return id == 0 ? BadRequest("Invalid user id") : Ok(await _unitOfWork.ApplicationUserManager.DeleteAsync(id)); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<ApplicationUser> jsonPatch)
        {
            try
            {
                return jsonPatch == null ? BadRequest("Invalid user") :Ok(await _unitOfWork.ApplicationUserManager.UpdateAsync(id, jsonPatch));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        [HttpPut("DisableOrEnable/{id}")]
        public async Task<IActionResult> DisableOrEnable(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || Guid.TryParse(id, out Guid res))
                    return BadRequest("invalid id format");
                return Ok(await _unitOfWork.ApplicationUserManager.DisableOrEnableUser(id));
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("download-apk")]
        public IActionResult DownloadApk()
        {
            //var client = new System.Net.Http.HttpClient();
            //var fileBytes = client.GetByteArrayAsync("https://github.com/GabryTm047/download-APK-PW1-2anno-_ITS/raw/refs/heads/main/app-release.apk").Result; 
            //return File(fileBytes, "application/vnd.android.package-archive", "GamePlanner.apk");

            return Ok("https://github.com/GabryTm047/download-APK-PW1-2anno-_ITS/raw/refs/heads/main/app-release.apk");
        }
        }
}
