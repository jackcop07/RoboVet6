using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Breed
{
    public class BreedToInsertDto
    {
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A SpeciesId is required")]
        public int SpeciesId { get; set; }

    }
}
