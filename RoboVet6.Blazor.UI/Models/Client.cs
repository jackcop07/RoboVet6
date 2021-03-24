using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoboVet6.Blazor.UI.Models
{
    public class Client
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "Address is too long.")]
        public string Address { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Post code is too long.")]
        public string Postcode { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City is too long.")]
        public string City { get; set; }

        public string HomePhone { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile phone must be 11 digits long.")]
        public string MobilePhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<Animal> Animals { get; set; }
        
    }
}
