using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Models
  {
  public class TaxUpdate
    {
    public decimal Tax { get; set; }
    public decimal NetPay { get; set; }
    public int CalculationId { get; set; }
    }
  }
