using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class SpeciesModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<AnimalModel> Animals { get; set; }

        public List<BreedModel> Breeds { get; set; }

        
    }
}
