using System.ComponentModel.DataAnnotations;

namespace RoboVet6.Blazor.UI.Models
{
    public class Animal
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SpeciesId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A breed must be selected.")]
        public int BreedId { get; set; }
    }
}
