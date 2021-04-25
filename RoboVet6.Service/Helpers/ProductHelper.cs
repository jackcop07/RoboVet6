using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Interfaces.Helpers;

namespace RoboVet6.Service.Helpers
{
    public class ProductHelper : IProductHelper
    {
        public decimal CalculatePriceExcVat(decimal taxRate, decimal priceIncVat)
        {
            return (priceIncVat / (100 + taxRate)) * 100;
        }
    }
}
