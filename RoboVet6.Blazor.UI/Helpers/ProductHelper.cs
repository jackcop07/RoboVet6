using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Interfaces.Helpers;

namespace RoboVet6.Blazor.UI.Helpers
{
    public class ProductHelper : IProductHelper
    {
        public decimal CalculatePriceExcVat(decimal taxRate, decimal priceWithVat)
        {
            var unformatted= (priceWithVat / (100 + taxRate)) * 100;

            var formatted = decimal.Round(unformatted, 2, MidpointRounding.ToPositiveInfinity);

            return formatted;
        }
    }
}
