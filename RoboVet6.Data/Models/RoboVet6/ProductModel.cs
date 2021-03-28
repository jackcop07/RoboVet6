using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal PriceIncVat { get; set; }

        [Required]
        public decimal PriceExcVat { get; set; }
    }
}
