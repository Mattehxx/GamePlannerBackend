using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationInputDTO model)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(model);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new { message   =  ex.Message });
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("multiple")]
        public async Task<IActionResult> MultipleCreate([FromBody] List<ReservationInputDTO> models)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(models);

                List<Reservation> entities = models.ConvertAll(_mapper.ToEntity);
                List<Reservation> createdEntities = [];

                Session session = await _unitOfWork.SessionManager.GetByIdAsync(models[0].SessionId);
                Event currentEvent = await _unitOfWork.EventManager.GetByIdAsync(session.EventId);

                foreach (Reservation entity in entities)
                {
                    await _unitOfWork.ReservationManager.CreateAsync(entity);

                    if (!currentEvent.IsPublic)
                    {
                        continue;
                    }

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

                if (await CanBeConfirmedAsync(deletedEntity))
                {
                    var reservation = await _unitOfWork.ReservationManager.GetFirstQueuedAsync(deletedEntity.SessionId);
                    if (reservation is not null)
                    {
                        await SendConfirmationEmailAsync(reservation);
                    }
                }

                return Ok(deletedEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Reservation> jsonPatch)
        {
            try
            {
                if (jsonPatch == null) return BadRequest("Invalid reservation");
                return Ok(await _unitOfWork.ReservationManager.PatchAsync(id, jsonPatch));
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
                ArgumentNullException.ThrowIfNull(sessionId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(token);

                Reservation reservation = await _unitOfWork.ReservationManager.GetBySessionAndUser(sessionId, userId);
                if (reservation.IsConfirmed && !reservation.IsDeleted) return BadRequest("Reservation already confirmed");

                if (!await CanBeConfirmedAsync(reservation))
                {
                    await SendQueuedEmailAsync(reservation);
                    return BadRequest("Session full");
                }

                reservation = await _unitOfWork.ReservationManager.ConfirmAsync(reservation, token);
                await SendDeleteEmailAsync(reservation);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("new-confirm-email")]
        public async Task<IActionResult> SendNewConfirmationEmail(int sessionId, string userId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sessionId);
                ArgumentNullException.ThrowIfNull(userId);

                Reservation reservation = await _unitOfWork.ReservationManager.GetBySessionAndUser(sessionId, userId);
                if (!reservation.IsNotified) return BadRequest("Reservation not alredy notified");

                if (!await SendConfirmationEmailAsync(reservation))
                {
                    return BadRequest("Cannot send confirmation email");
                }
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

            await _emailService.SendDeleteEmailAsync(user.Email, user.Name, entity.ReservationId);

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
            var confirmedReservations = await _unitOfWork.ReservationManager.GetConfirmedAsync(entity.SessionId);

            if (confirmedReservations is null || confirmedReservations.Count() >= session.Seats) return false;

            return true;
        }

        #endregion
    }
}
