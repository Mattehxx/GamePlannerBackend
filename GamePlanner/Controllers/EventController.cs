using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO;
using GamePlanner.DTO.InputDTO;
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
        private readonly UnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public EventController(UnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventInputDTO model)
        {
            try
            {
              if (model == null) return BadRequest("No event found");
              Event entity = _mapper.ToEntity(model);
              await _unitOfWork.EventManager.CreateAsync(entity);
              return (await _unitOfWork.Commit()).Value ? Ok(_mapper.ToModel(entity)) : BadRequest("Event impossible to create");
            }catch (Exception ex) 
            {
              return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
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

        [HttpGet]
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
