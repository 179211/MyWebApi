using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
