using GamePlanner.DAL.Data.Db;
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
    public class RecurrenceController : ODataController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public RecurrenceController( IUnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecurrenceInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("No event found");
                Recurrence entity = _mapper.ToEntity(model);
                await _unitOfWork.RecurrenceManager.CreateAsync(entity);
                return (await _unitOfWork.Commit()).Value ? Ok(_mapper.ToModel(entity)) : BadRequest("Recurrence impossible to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Recurrence> jsonPatch)
        {
            try
            {
                Recurrence updatedEntity = await _unitOfWork.RecurrenceManager.UpdateAsync(id, jsonPatch);
                return (await _unitOfWork.Commit()).Value ? Ok(updatedEntity) : BadRequest("Recurrence impossible to create");
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
                return oDataQueryOptions != null ? Ok(await _unitOfWork.RecurrenceManager.GetAsync(oDataQueryOptions)) : BadRequest("No recurrence found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
