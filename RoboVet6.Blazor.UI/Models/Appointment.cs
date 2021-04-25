using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoboVet6.Blazor.UI.Models
{
    public class Appointment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [Required]
        public int DiaryId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int AppointmentLength { get; set; }

        public string Notes { get; set; }
    }
}
