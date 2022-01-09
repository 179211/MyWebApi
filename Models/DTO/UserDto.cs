using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
