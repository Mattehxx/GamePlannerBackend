using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using GamePlanner.Services.IServices;
using GamePlanner.DAL.Data.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(UserManager<ApplicationUser> userManager, UnitOfWork unitOfWork, 
        Mapper mapper, IEmailService emailService) : ODataController
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        private readonly Mapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Reservation> options)
        {
            try
            {
                return Ok(_unitOfWork.ReservationManager.Get(options));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservation model)
        {
            try
            {
                var entity = await _unitOfWork.ReservationManager.CreateAsync(model/*_mapper.ToEntity(model)*/);
                
                var result = await SendConfirmationEmailAsync(entity);
                if (!result)
                {
                    await _unitOfWork.ReservationManager.DeleteAsync(entity.ReservationId);
                    return BadRequest("User not found or email not valid");
                }

                return Ok(entity);
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
                var deletedEntity = await _unitOfWork.ReservationManager.DeleteAsync(id);
                return Ok(deletedEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Reservation> jsonPatch)
        {
            try
            {
                var updatedEntity = await _unitOfWork.ReservationManager.UpdateAsync(id, jsonPatch);
                return Ok(updatedEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Custom

        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmReservation(int sessionId, string userId, string token)
        {
            try
            {
                await _unitOfWork.ReservationManager.ConfirmAsync(sessionId, userId, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region Utility

        private async Task<bool> SendConfirmationEmailAsync(Reservation entity)
        {
            var user = await _userManager.FindByIdAsync(entity.UserId);

            if (user is null || user.Email is null) return false;

            await _emailService.SendConfirmationEmailAsync(user.Email, entity.SessionId, user.Id, entity.Token);

            return true;
        }

        #endregion
    }
}
