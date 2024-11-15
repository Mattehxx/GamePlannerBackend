using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,
        IMapper mapper, IEmailService emailService) : ODataController
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Reservation> options)
        {
            try
            {
                if (options == null) return BadRequest("No options found");
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
                Reservation entity = await _unitOfWork.ReservationManager.CreateAsync(_mapper.ToEntity(model));

                if (!await CanBeConfirmedAsync(entity))
                {
                    await SendQueuedEmailAsync(entity);
                }
                else
                {
                    if (!await SendConfirmationEmailAsync(entity))
                    {
                        await _unitOfWork.ReservationManager.DeleteAsync(entity.ReservationId);
                        return BadRequest("User not found or email not valid");
                    }
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MultipleCreate([FromBody] List<ReservationInputDTO> models)
        {
            try
            {
                List<Reservation> entities = models.ConvertAll(_mapper.ToEntity);
                List<Reservation> createdEntities = [];

                foreach (Reservation entity in entities)
                {

                    if (!await CanBeConfirmedAsync(entity))
                    {
                        await SendQueuedEmailAsync(entity);
                    }
                    else
                    {
                        if (!await SendConfirmationEmailAsync(entity))
                        {
                            await _unitOfWork.ReservationManager.DeleteAsync(entity.ReservationId);
                        }
                        else
                        {
                            createdEntities.Add(entity);
                        }
                    }
                }

                return Ok(createdEntities);
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

                Reservation reservation = await _unitOfWork.ReservationManager.GetFirstQueuedAsync(deletedEntity.SessionId);
                await SendConfirmationEmailAsync(reservation);

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
                if (jsonPatch == null) return BadRequest("Invalid reservation");
                return Ok(await _unitOfWork.ReservationManager.UpdateAsync(id, jsonPatch));
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

            await _unitOfWork.ReservationManager.ConfirmNotificationAsync(entity);

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
            if (user is null || user.Email is null) return false;

            Session session = await _unitOfWork.SessionManager.GetByIdAsync(entity.SessionId);
            Event currentEvent = await _unitOfWork.EventManager.GetByIdAsync(session.EventId);

            await _emailService.SendQueuedEmailAsync(user.Email, user.Name, currentEvent.Name);

            return true;
        }

        private async Task<bool> CanBeConfirmedAsync(Reservation entity)
        {
            Session session = await _unitOfWork.SessionManager.GetByIdAsync(entity.SessionId);
            var reservations = await _unitOfWork.ReservationManager.GetConfirmedAsync(entity.SessionId);

            if (reservations is null || reservations.Count() >= session.Seats) return false;

            return true;
        }

        #endregion
    }
}
