using System;
using System.Collections.Generic;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Animal
{
    public class AnimalToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
    }
}
