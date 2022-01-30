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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

       
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User model)
        {
            var user = await _userRepository.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        public async Task<IActionResult> GetUsers()
        {
            var objList = await _userRepository.GetUsersAsync();
            var objDto = new List<UserDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<UserDto>(obj));
            }
            return Ok(objDto);
        }


    }
}
