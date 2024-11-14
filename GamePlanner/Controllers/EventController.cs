using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO;
using GamePlanner.DTO.InputDTO;
using GamePlanner.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ODataController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public EventController(IUnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOdata(ODataQueryOptions<Event> oDataQueryOptions)
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
        public async Task<IActionResult> Create(EventInputDTO model)
        {
            try
            {
                throw new NotImplementedException();
                //if (model == null) return BadRequest("Invalid event");
                //Event entity = _mapper.ToEntity(model);
                //await _unitOfWork.EventManager.CreateAsync(entity);
                //return (await _unitOfWork.Commit()).Value ? Ok(_mapper.ToModel(entity)) : BadRequest("Event creation failed");
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
                throw new NotImplementedException();
                //Event updatedEntity = await _unitOfWork.EventManager.UpdateAsync(id, jsonPatch);
                //return (await _unitOfWork.Commit()).Value ? Ok(updatedEntity) : BadRequest("Event update failed");
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
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
