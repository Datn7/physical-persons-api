using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace physical_persons_api.DTOs
{
    public class PersonCreationDTO
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile Picture { get; set; }
    }
}
