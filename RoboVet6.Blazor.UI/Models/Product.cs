using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace RoboVet6.Blazor.UI.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Price including VAT must be 0.00 or greater.")]
        public decimal PriceIncVat { get; set; }
        public decimal PriceExcVat { get; set; }
    }
}
