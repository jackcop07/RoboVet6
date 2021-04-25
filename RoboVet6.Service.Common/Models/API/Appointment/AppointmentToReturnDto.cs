using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Appointment
{
    public class AppointmentToReturnDto
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
