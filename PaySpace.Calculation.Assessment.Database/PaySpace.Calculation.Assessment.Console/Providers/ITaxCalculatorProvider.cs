using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;

namespace PaySpace.Calculation.Assessment.Console.Providers
  {
  public interface ITaxCalculatorProvider
    {
    ITaxCalculatorService GetTaxCalculatorService(int countryId);
    }
  }
