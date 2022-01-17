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
            ShouldMapProperty = t => true;
            
            SourceMemberNamingConvention = new PascalCaseNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.dtCreated))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.dtUpdated))
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.dtCreated))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.dtUpdated))
                .ReverseMap();

        }
    }
}
