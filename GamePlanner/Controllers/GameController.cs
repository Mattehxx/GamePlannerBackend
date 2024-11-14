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
    public class GameController(UnitOfWork unitOfWork, Mapper mapper) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly Mapper _mapper = mapper;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Game> options)
        {
            try
            {
                return Ok(_unitOfWork.GameManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameInputDTO model)
        {
            try
            {
                var entity = await _unitOfWork.GameManager.CreateAsync(_mapper.ToEntity(model));
                return Ok(entity);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete,Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedEntity = await _unitOfWork.GameManager.DeleteAsync(id);
                return Ok(_mapper.ToModel(deletedEntity));
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Game> jsonPatch)
        {
            try
            {
                var updatedEntity = await _unitOfWork.GameManager.UpdateAsync(id, jsonPatch);
                return Ok(_mapper.ToModel(updatedEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        [HttpPut,Route("Disable/{id}/{confirm}")]
        public async Task<IActionResult> Disable(int id,bool confirm)
        {
            try
            {
                var res = await _unitOfWork.GameManager.DisableGame(id, confirm);
                return Ok(_mapper.ToModel(res));
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
