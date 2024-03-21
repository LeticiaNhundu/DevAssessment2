using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService
{
    public interface ITaxCalculatorService
    {
    decimal CalculateTax(decimal income);
    }
}
