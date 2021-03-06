﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Animal
{
    public class AnimalToUpdateDto
    {
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Client Id is required")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "A Species Id is required")]
        public int SpeciesId { get; set; }

        [Required(ErrorMessage = "A Breed Id is required")]
        public int BreedId { get; set; }


    }
}
