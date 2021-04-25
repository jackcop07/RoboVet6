using System;
using System.Collections.Generic;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Product
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceIncVat { get; set; }
        public decimal PriceExcVat { get; set; }
    }
}
