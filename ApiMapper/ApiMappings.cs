using AutoMapper;
using MyWebApi.Models;
using MyWebApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.ApiMapper
{
    public class ApiMappings : Profile
    {
        public ApiMappings()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
