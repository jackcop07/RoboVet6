using System;
using System.Collections.Generic;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Product
{
    public class ProductToUpdateDto
    {
        public string Name { get; set; }
        public decimal PriceIncVat { get; set; }
    }
}
