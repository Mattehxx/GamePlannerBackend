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
    public class SessionController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public SessionController(UnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Session> options)
        {
            try
            {
                return Ok(_unitOfWork.SessionManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Session model)
        {
            try
            {
                var entity = await _unitOfWork.SessionManager.CreateAsync(model/*_mapper.ToEntity(model)*/);
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
                var deletedEntity = await _unitOfWork.SessionManager.DeleteAsync(id);
                return Ok(deletedEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Session> jsonPatch)
        {
            try
            {
                var updatedEntity = await _unitOfWork.SessionManager.UpdateAsync(id, jsonPatch);
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
