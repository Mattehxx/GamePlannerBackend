using Azure;
using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EventController : ODataController
    {
        private readonly GamePlannerDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        public EventController(GamePlannerDbContext context,UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("events")]
        public async Task<IActionResult> Create(Event entity)
        {
            try
            {
              if (entity == null) return BadRequest("No event found");
              await _unitOfWork.EventManager.CreateAsync(entity);
              return (await _unitOfWork.Commit()).Value ? Ok(entity) : BadRequest("Event impossible to create");
            }catch (Exception ex) 
            {
              return BadRequest(ex.Message);
            }
        }

        [HttpPatch("events/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Event> jsonPatch)
        {
            try
            {
              Event updatedEntity = await _unitOfWork.EventManager.UpdateAsync(id,jsonPatch);
              return (await _unitOfWork.Commit()).Value ? Ok(updatedEntity) : BadRequest("Event impossible to create");
            }
            catch (Exception ex)
            {
              return BadRequest(ex.Message);
            }
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetOdata(ODataQueryOptions oDataQueryOptions)
        {
           try
            {
              return oDataQueryOptions != null ? Ok(await _unitOfWork.EventManager.GetAsync(oDataQueryOptions)) : BadRequest("No event found");
            }
            catch (Exception ex)
            {
              return BadRequest(ex.Message);
            }
        }
    }
}
