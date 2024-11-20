using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using GamePlanner.Services.IServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService,
        IBlobService blobService) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;
        private readonly IBlobService _blobService = blobService;

        #region CRUD

        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Event> oDataQueryOptions)
        {
            try
            {
                return oDataQueryOptions != null 
                    ? Ok(_unitOfWork.EventManager.Get(oDataQueryOptions)) 
                    : BadRequest("No event found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EventInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("Invalid event");
                return Ok(await _unitOfWork.EventManager.CreateAsync(_mapper.ToEntity(model)));
             }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Event> jsonPatch)
        {
            try
            {
                if (jsonPatch == null) return BadRequest("Invalid event");

                if (ContainsIsPublicOperation(jsonPatch)) await NotifyUsers(id);

                return Ok(await _unitOfWork.EventManager.PatchAsync(id, jsonPatch));
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
                if(id == 0) return BadRequest("Invalid event");
                return Ok(await _unitOfWork.EventManager.DeleteAsync(id));
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        [HttpPut("image/{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            try
            {
                var currentEvent = await _unitOfWork.EventManager.GetByIdAsync(id);
                if (currentEvent == null) return BadRequest("User not found");

                var containerClient = _blobService.GetBlobContainerClient("event-container");
                var imageUrl = await _blobService.UploadFileAsync(containerClient, file);
                currentEvent.ImgUrl = imageUrl;

                return Ok(await _unitOfWork.EventManager.UpdateAsync(currentEvent));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #region Utility

        private bool ContainsIsPublicOperation(JsonPatchDocument<Event> jsonPatch)
        {
            return jsonPatch.Operations
                .Any(op => op.path == "/isPublic" && op.from == "false" && op.value.ToString() == "true");
        }

        private async Task NotifyUsers(int id)
        {
            var sessions = _unitOfWork.SessionManager.GetAll()
                .Where(s => s.EventId == id && !s.IsDeleted)
                .ToList();

            foreach (var session in sessions)
            {
                var reservations = _unitOfWork.ReservationManager.GetAll()
                    .Include(r => r.User)
                    .Where(r => r.SessionId == session.SessionId
                    && !r.IsDeleted
                    && !r.IsNotified
                    && !r.IsConfirmed)
                    .ToList();

                foreach (var reservation in reservations)
                {
                    if (reservation.User is not null)
                    {
                        await _emailService.SendConfirmationEmailAsync(
                            reservation.User.Email!,
                            reservation.User.Name,
                            session.SessionId,
                            reservation.UserId,
                            reservation.Token
                        );
                    }
                }
            }
        }

        #endregion
    }
}
