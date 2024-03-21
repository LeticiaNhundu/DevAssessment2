using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Models
  {
  public class TaxBracket
    {
    public decimal LowerLimit { get; set; }
    public decimal UpperLimit { get; set; }
    public decimal Rate { get; set; }
    }
  }
