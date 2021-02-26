using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboVet6.Blazor.UI.Models
{
    public class Client
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        
    }
}
