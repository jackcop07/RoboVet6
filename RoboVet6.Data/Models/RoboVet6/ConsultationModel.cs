using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class ConsultationModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime ConsultationDate { get; set; }

        [Required]
        public string Employee { get; set; }

        [Required]
        [ForeignKey("AnimalModel")]
        public int AnimalId { get; set; }

        public IEnumerable<ConsultationDetailModel> ConsultationDetails { get; set; }

        [Required] 
        public decimal SaleTotalIncVat { get; set; } = 0;

        [Required] 
        public decimal SaleTotalExcVat { get; set; } = 0;
    }
}
