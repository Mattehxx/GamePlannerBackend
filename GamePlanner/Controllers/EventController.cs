using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.Mapper;
using GamePlanner.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(IUnitOfWork unitOfWork, IMapper mapper) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

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
        public async Task<IActionResult> Create(EventInputDTO model)
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
                return Ok(await _unitOfWork.EventManager.UpdateAsync(id, jsonPatch));
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
    }
}
