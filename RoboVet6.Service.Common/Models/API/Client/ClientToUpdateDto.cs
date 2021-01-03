using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Client
{
    public class ClientToUpdateDto
    {
        [Required(ErrorMessage = "A Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "An Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "A Postcode is required")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "A City is required")]
        public string City { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }
    }
}
