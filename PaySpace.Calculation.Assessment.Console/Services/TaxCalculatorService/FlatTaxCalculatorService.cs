using PaySpace.Calculation.Assessment.Console.Providers;

namespace PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService
  {
  public class FlatTaxCalculatorService : ITaxCalculatorService
    {
    private decimal _flatRate;
    private decimal _minimumThreshold;

    public FlatTaxCalculatorService(decimal flatRate,decimal minimumThreshold)
      {
      _flatRate = flatRate;
      _minimumThreshold = minimumThreshold;

      }
    public decimal CalculateTax(decimal income)
      {
      decimal tax = 0;

      if (income > _minimumThreshold)
        {
        tax = _flatRate;
        }
      return tax;
      }
    }
  }
