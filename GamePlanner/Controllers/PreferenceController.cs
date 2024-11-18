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
    public class PreferenceController(IUnitOfWork unitOfWork, IMapper mapper) : ODataController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Preference> options)
        {
            try
            {
                if (options == null) return BadRequest("No options found");
                return Ok(_unitOfWork.PreferenceManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PreferenceInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("Invalid preference");
                return Ok(await _unitOfWork.PreferenceManager.CreateAsync(_mapper.ToEntity(model)));
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
                if (id == 0) return BadRequest("Invalid preference");
                return Ok(await _unitOfWork.PreferenceManager.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Preference> jsonPatch)
        {
            try
            {
                if (jsonPatch == null) return BadRequest("Invalid preference");
                return Ok(await _unitOfWork.PreferenceManager.PatchAsync(id, jsonPatch));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
