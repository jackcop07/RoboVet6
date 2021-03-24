using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Breed
{
    public class BreedToUpdateDto
    {
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }
    }
}
