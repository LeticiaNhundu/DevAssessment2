using PaySpace.Calculation.Assessment.Console.Models;


namespace PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService
{
  public class ProgressiveTaxCalculatorService : ITaxCalculatorService
    {
    private List<TaxBracket> taxBrackets = new List<TaxBracket>();
    
    public ProgressiveTaxCalculatorService(List<TaxBracket> taxBrackets)
      {
      this.taxBrackets = taxBrackets;
      }

    public decimal CalculateTax(decimal income)
      {
      decimal taxAmount = 0;
      foreach(var taxBracket in taxBrackets) {
          if(income > taxBracket.LowerLimit){
          decimal taxableAmount = Math.Min(income - taxBracket.LowerLimit, taxBracket.UpperLimit - taxBracket.LowerLimit);//Math.Min(income, taxBracket.UpperLimit) - taxBracket.LowerLimit;
          taxAmount += taxableAmount * (taxBracket.Rate / 100);
          continue;
          }
      }
      return taxAmount;
      }
    }
  }
