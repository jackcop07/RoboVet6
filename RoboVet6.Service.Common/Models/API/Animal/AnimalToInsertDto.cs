using System.ComponentModel.DataAnnotations;

namespace RoboVet6.Service.Common.Models.API.Animal
{
    public class AnimalToInsertDto
    {
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A SpeciesId is required")]
        public int SpeciesId { get; set; }
    }
}
