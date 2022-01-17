using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Models.DTO;
using MyWebApi.Repository.IRepository;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("{departmentId:int}", Name = "GetDepartment")]
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
            return CreatedAtRoute("GetDepartment", new { departmentId = departmentObj.Id }, departmentObj);
        }

        [HttpPatch("{departmentId:int}", Name = "UpdateDepartment")]
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





    }
}
