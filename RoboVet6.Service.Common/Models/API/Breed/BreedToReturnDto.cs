using System;
using System.Collections.Generic;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Breed
{
    public class BreedToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpeciesId { get; set; }
    }
}
