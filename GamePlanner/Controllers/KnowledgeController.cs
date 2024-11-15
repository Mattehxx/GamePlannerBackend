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
    public class KnowledgeController(IUnitOfWork unitOfWork, IMapper mapper) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Knowledge> options)
        {
            try
            {
                if (options == null) return BadRequest("No options found");
                return Ok(_unitOfWork.KnowledgeManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KnowledgeInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("Invalid knowledge");
                return Ok(await _unitOfWork.KnowledgeManager.CreateAsync(_mapper.ToEntity(model)));
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
               if(id == 0) return BadRequest("Invalid knowledge id");
               return Ok(await _unitOfWork.KnowledgeManager.DeleteAsync(id));
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Knowledge> jsonPatch)
        {
            try
            {
                if(jsonPatch == null) return BadRequest("Invalid knowledge");
                return Ok(await _unitOfWork.KnowledgeManager.UpdateAsync(id, jsonPatch));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
