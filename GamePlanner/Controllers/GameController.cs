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
    public class GameController(IUnitOfWork unitOfWork, IMapper mapper) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Game> options)
        {
            try
            {
                if (options == null) return BadRequest("No options found");
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
                if (model == null) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.CreateAsync(await _mapper.ToEntity(model)));
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
                if (id == 0) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.DeleteAsync(id));
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
                if (jsonPatch == null) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.UpdateAsync(id, jsonPatch));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        [HttpPut("DisableOrEnable/{id}")]
        public async Task<IActionResult> DisableOrEnable(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Invalid game");
                return Ok(await _unitOfWork.GameManager.DisableOrEnableGame(id));
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
