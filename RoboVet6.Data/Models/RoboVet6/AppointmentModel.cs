using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class AppointmentModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("AnimalModel")]
        public int AnimalId { get; set; }
        
        public AnimalModel Animal { get; set; }

        [Required]
        [ForeignKey("DiaryModel")]
        public int DiaryId { get; set; }

        public DiaryModel Diary { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int AppointmentLength { get; set; }

        public string Notes { get; set; }

    }
}
