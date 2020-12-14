using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoboVet6.Data.Models
{
    public class Animal
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
