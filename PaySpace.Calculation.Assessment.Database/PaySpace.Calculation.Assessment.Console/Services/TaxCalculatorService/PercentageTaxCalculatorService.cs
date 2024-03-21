using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService
{
  public class PercentageTaxCalculatorService : ITaxCalculatorService
    {
    private decimal percentage;

    public PercentageTaxCalculatorService(decimal percentage){
    this.percentage = percentage;
    }

    public decimal CalculateTax(decimal income)
      {
      if (income <= 0)
        {
        return 0;
        }
      else
        {
        return income * (percentage / 100);
        }
      }
    }
  }
