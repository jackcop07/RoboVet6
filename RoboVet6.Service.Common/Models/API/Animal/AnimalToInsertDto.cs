using System.ComponentModel.DataAnnotations;

namespace RoboVet6.Service.Common.Models.API.Animal
{
    public class AnimalToInsertDto
    {
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }
    }
}
