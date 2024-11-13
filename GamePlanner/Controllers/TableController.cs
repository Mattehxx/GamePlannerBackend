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
    public class TableController : ODataController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public TableController(UnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region CRUD
        [HttpPost]
        public async Task<IActionResult> Create(TableInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("No table found");
                Table entity = _mapper.ToEntity(model);
                await _unitOfWork.TableManager.CreateAsync(entity);
                return (await _unitOfWork.Commit()).Value ? Ok(_mapper.ToModel(entity)) : BadRequest("Table impossible to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Table> jsonPatch)
        {
            try
            {
                Table updatedEntity = await _unitOfWork.TableManager.UpdateAsync(id, jsonPatch);
                return (await _unitOfWork.Commit()).Value ? Ok(updatedEntity) : BadRequest("Table impossible to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOdata(ODataQueryOptions oDataQueryOptions)
        {
            try
            {
                return oDataQueryOptions != null ? Ok(await _unitOfWork.TableManager.GetAsync(oDataQueryOptions)) : BadRequest("No table found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Custom
        [HttpGet,Route("Availability/{startDate}/{endDate}")]
        public async Task<IActionResult> GetTablesAvailability(DateTime startDate,DateTime endDate)
        {
            try
            {
                return Ok(await _unitOfWork.TableManager.GetTablesAvailability(startDate, endDate));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
