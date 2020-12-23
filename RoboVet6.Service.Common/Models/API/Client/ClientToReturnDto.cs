using System.Collections.Generic;
using RoboVet6.Service.Common.Models.API.Animal;

namespace RoboVet6.Service.Common.Models.API.Client
{
    public class ClientToReturnDto
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
        public List<AnimalToReturnDto> Animals { get; set; }
    }
}
