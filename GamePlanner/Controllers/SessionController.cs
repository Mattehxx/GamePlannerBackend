using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController(IUnitOfWork unitOfWork, IMapper mapper) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #region CRUD
        
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Session> options)
        {
            try
            {
                if (options == null) return BadRequest("No options found");
                return Ok(_unitOfWork.SessionManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("Invalid session");
                if (model.StartDate.Date != model.EndDate.Date) return BadRequest("start date and end must be in the same day");
                return Ok(await _unitOfWork.SessionManager.CreateAsync(_mapper.ToEntity(model)));
            }
            catch (Exception ex)
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
                if (id == 0) return BadRequest("Invalid session");
                return Ok(await _unitOfWork.SessionManager.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Session> jsonPatch)
        {
            try
            {
                if (id == 0) return BadRequest("Invalid session");
                return Ok(await _unitOfWork.SessionManager.PatchAsync(id, jsonPatch));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region CUSTOM
        /// <summary>
        /// DA RIFARE CON MODELLI
        /// </summary>
        /// <returns></returns>
        //[NonAction]

        [HttpGet, Route("upcoming")]
        public async Task<IActionResult> GetUpcomingSessions()  //da rifare con mapper e dto
        {
            try
            {
                var sessions = _unitOfWork.SessionManager.GetUpcomingSessions();
                if (sessions == null) return BadRequest();
                if (await sessions.AllAsync(s => s.Event != null && s.Game != null))
                {
                    return Ok(await sessions.Select(s => new
                    {
                        s.SessionId,
                        Master = s.Master != null ? new
                        {
                            s.MasterId,
                            s.Master.Name, 
                            s.Master.Surname,
                            s.Master.Email,
                            s.Master.ImgUrl,
                        } : null,
                        Event = new
                        {
                            s.Event!.EventId,   //controllo nell'if
                            s.Event.Name,
                            s.Event.Description,
                        },
                        Game = new
                        {
                            s.Game!.Name,
                            s.Game.Description,
                            s.Game.ImgUrl
                        },
                        s.StartDate,
                        s.EndDate,
                        s.Seats
                    }).ToListAsync());
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
