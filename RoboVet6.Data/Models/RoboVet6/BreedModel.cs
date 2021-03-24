using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class BreedModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("SpeciesModel")]
        public int SpeciesId { get; set; }

        public SpeciesModel Species { get; set; }
    }
}
