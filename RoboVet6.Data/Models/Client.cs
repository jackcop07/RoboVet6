using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Data.Models
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        [Required]
        public string Email { get; set; }
        public string WorkPhone { get; set; }
    }
}
