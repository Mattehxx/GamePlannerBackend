using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public KnowledgeController(UnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Knowledge> options)
        {
            try
            {
                return Ok(_unitOfWork.KnowledgeManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Knowledge model)
        {
            try
            {
                var entity = await _unitOfWork.KnowledgeManager.CreateAsync(model /*_mapper.ToEntity(model)*/);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedEntity = await _unitOfWork.KnowledgeManager.DeleteAsync(id);
                return Ok(deletedEntity);
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
                var updatedEntity = await _unitOfWork.KnowledgeManager.UpdateAsync(id, jsonPatch);
                return Ok(updatedEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
