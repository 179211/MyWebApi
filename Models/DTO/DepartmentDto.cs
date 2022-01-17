using AutoMapper.Configuration.Annotations;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models.DTO
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        //[SourceMember(nameof(Department.dtCreated))]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
