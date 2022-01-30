using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Models.DTO;
using MyWebApi.Repository.IRepository;

namespace MyWebApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all departments 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DepartmentDto>))]
        public async Task<IActionResult> GetDepartments()
        {
            var objList = await _departmentRepository.GetDepartmentsAsync();
            var objDto = new List<DepartmentDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<DepartmentDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get specific department with department id
        /// </summary>
        /// <param name="departmentId">The id of the department</param>
        /// <returns></returns>
        [HttpGet("{departmentId:int}", Name = "GetDepartment")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(DepartmentDto))]
        public IActionResult GetDepartment(int departmentId)
        {
            var obj = _departmentRepository.GetDepartment(departmentId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<DepartmentDto>(obj);
            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_departmentRepository.DepartmentExists(departmentDto.Name))
            {
                ModelState.AddModelError("", "Department Exists!");
                return StatusCode(404, ModelState);
            }
            departmentDto.Created = DateTime.Now;
            departmentDto.Updated = DateTime.Now;
            var departmentObj = _mapper.Map<Department>(departmentDto);
            if (!_departmentRepository.CreateDepartment(departmentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {departmentObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetDepartment", new { 
                Version = HttpContext.GetRequestedApiVersion().ToString(), 
                departmentId = departmentObj.Id 
            }, departmentObj);
        }

        [HttpPatch("{departmentId:int}", Name = "UpdateDepartment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null || departmentId != departmentDto.Id)
            {
                return BadRequest(ModelState);
            }

            var departmentObj = _mapper.Map<Department>(departmentDto);
            if (!_departmentRepository.UpdateDepartment(departmentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {departmentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{departmentId:int}", Name = "DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
            {
                return NotFound();
            }

            var departmentObj = _departmentRepository.GetDepartment(departmentId);
            if (!_departmentRepository.DeleteDepartment(departmentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {departmentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        /// <summary>
        /// Get list of all departments 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        //[HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DepartmentDto>))]
        public async Task<IActionResult> GetDepartments2()
        {
            var objList = await _departmentRepository.GetDepartmentsAsync();
            var objDto = new List<DepartmentDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<DepartmentDto>(obj));
            }
            return Ok(objDto);
        }



    }
}
