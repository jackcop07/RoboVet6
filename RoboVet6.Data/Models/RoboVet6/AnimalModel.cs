using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class AnimalModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        [ForeignKey("ClientModel")]
        public int ClientId { get; set; }

        public ClientModel Client { get; set; }

        [Required]
        [ForeignKey("SpeciesModel")]
        public int SpeciesId { get; set; }

        [Required]
        [ForeignKey("BreedModel")]
        public int BreedId { get; set; }

        [Required]
        [ForeignKey("ColourModel")]
        public int ColourId { get; set; }


    }
}
