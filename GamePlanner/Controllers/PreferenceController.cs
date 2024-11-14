﻿using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GamePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController(UnitOfWork unitOfWork, Mapper mapper) : ODataController
    {
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        private readonly Mapper _mapper = mapper;

        #region CRUD
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Preference> options)
        {
            try
            {
                return Ok(_unitOfWork.PreferenceManager.Get(options));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Preference model)
        {
            try
            {
                var entity = await _unitOfWork.PreferenceManager.CreateAsync(model /*_mapper.ToEntity(model)*/);
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
                var deletedEntity = await _unitOfWork.PreferenceManager.DeleteAsync(id);
                return Ok(deletedEntity);
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
                var updatedEntity = await _unitOfWork.PreferenceManager.UpdateAsync(id, jsonPatch);
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
