using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO;
using GamePlanner.DTO.InputDTO;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
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
    public class ReservationController(UserManager<ApplicationUser> userManager, UnitOfWork unitOfWork, 
        Mapper mapper, IEmailService emailService) : ODataController
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
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
        public async Task<IActionResult> Create([FromBody] ReservationInputDTO model)
        {
            try
            {
                Reservation entity = _mapper.ToEntity(model);

                if (!await CanBeConfirmedAsync(entity))
                {
                    await SendQueuedEmailAsync(entity);
                } 
                else
                {
                    entity = await _unitOfWork.ReservationManager.CreateAsync(entity);
                
                    if (!await SendConfirmationEmailAsync(entity))
                    {
                        await _unitOfWork.ReservationManager.DeleteAsync(entity.ReservationId);
                        return BadRequest("User not found or email not valid");
                    }
                }

                return Ok(_mapper.ToModel(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(id);
                var deletedEntity = await _unitOfWork.ReservationManager.DeleteAsync(id);

                Reservation reservation = await GetFirstQueuedAsync(deletedEntity.SessionId);
                await SendConfirmationEmailAsync(reservation);

                return Ok(_mapper.ToModel(deletedEntity));
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
                return Ok(_mapper.ToModel(updatedEntity));
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
                Reservation reservation = await _unitOfWork.ReservationManager.GetBySessionAndUser(sessionId, userId);
                
                reservation = await _unitOfWork.ReservationManager.ConfirmAsync(reservation, token);
                
                await SendDeleteEmailAsync(reservation);
                
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

            await _emailService.SendConfirmationEmailAsync(user.Email, user.Name, entity.SessionId, user.Id, entity.Token);
            
            entity.IsNotified = true;
            _unitOfWork._context.Update(entity);
            await _unitOfWork._context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> SendDeleteEmailAsync(Reservation entity)
        {
            var user = await _userManager.FindByIdAsync(entity.UserId);

            if (user is null || user.Email is null) return false;

            await _emailService.SendDeleteEmailAsync(user.Email, user.Name, entity.ReservationId, entity.Token);

            return true;
        }

        private async Task<bool> SendQueuedEmailAsync(Reservation entity)
        {
            var user = await _userManager.FindByIdAsync(entity.UserId);
            Session session = await _unitOfWork._context.Sessions
                .Include(s => s.Event)
                .Where(s => s.SessionId == entity.SessionId)
                .SingleAsync();
            if (session.Event is null) return false; 

            if (user is null || user.Email is null) return false;

            await _emailService.SendQueuedEmailAsync(user.Email, user.Name, session.Event.Name);

            return true;
        }

        private async Task<bool> CanBeConfirmedAsync(Reservation entity)
        {
            var session = await _unitOfWork._context.Sessions.SingleAsync(s => s.SessionId == entity.SessionId);
            if (session is null) return false;

            var reservations = await _unitOfWork._context.Reservations
                .Where(r => r.SessionId == session.SessionId && !r.IsDeleted && r.IsConfirmed)
                .ToListAsync();

            if (reservations is null || reservations.Count >= session.Seats) return false;
            
            return true;
        }

        private async Task<Reservation> GetFirstQueuedAsync(int sessionId)
        {
            return await _unitOfWork._context.Reservations
                .Where(r => r.SessionId == sessionId && !r.IsDeleted && !r.IsConfirmed && !r.IsNotified)
                .OrderBy(r => r.ReservationId)
                .FirstAsync();
        }

        #endregion
    }
}
