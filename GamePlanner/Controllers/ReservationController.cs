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
    public class ReservationController : ODataController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public ReservationController(UnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("No event found");
                Reservation entity = _mapper.ToEntity(model);
                await _unitOfWork.ReservationManager.CreateAsync(entity);
                return (await _unitOfWork.Commit()).Value ? Ok(_mapper.ToModel(entity)) : BadRequest("Reservation impossible to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<Reservation> jsonPatch)
        {
            try
            {
                Reservation updatedEntity = await _unitOfWork.ReservationManager.UpdateAsync(id, jsonPatch);
                return (await _unitOfWork.Commit()).Value ? Ok(updatedEntity) : BadRequest("Reservation impossible to create");
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
                return oDataQueryOptions != null ? Ok(await _unitOfWork.ReservationManager.GetAsync(oDataQueryOptions)) : BadRequest("No reservation found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
