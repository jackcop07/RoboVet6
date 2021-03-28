using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Text;

namespace RoboVet6.Data.Models.RoboVet6
{
    public class ConsultationDetailModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ConsultationModel")]
        public int ConsultationId { get; set; }
        public ConsultationModel Consultation { get; set; }

        [Required]
        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        [Required]
        public long Quantity { get; set; }

        public decimal Discount { get; set; } = 0;

        [Required]
        public decimal PriceIncVat { get; set; }

        [Required]
        public decimal PriceExcVat { get; set; }

    }
}
