using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboVet6.Blazor.UI.Interfaces.Helpers
{
    public interface IProductHelper
    {
        decimal CalculatePriceExcVat(decimal taxRate, decimal priceWithVat);
    }
}
