using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoboVet6.Service.Common.Interfaces.Helpers
{
    public interface IProductHelper
    {
        decimal CalculatePriceExcVat(decimal taxRate, decimal priceIncVat);
    }
}
