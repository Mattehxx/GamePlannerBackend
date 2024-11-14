using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController(UnitOfWork unitOfWork, Mapper mapper) : ODataController
    {
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        private readonly Mapper _mapper = mapper;

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
                return Ok(_mapper.ToModel(entity));
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
                return Ok(_mapper.ToModel(deletedEntity));
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
                return Ok(_mapper.ToModel(updatedEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
