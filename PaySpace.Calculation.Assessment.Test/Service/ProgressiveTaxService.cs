using PaySpace.Calculation.Assessment.Console.Models;
using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;

namespace PaySpace.Calculation.Assessment.Test.Service
  {
  [TestFixture]
  public class ProgressiveTaxService
    {
    private ProgressiveTaxCalculatorService? taxCalculatorService;

    [SetUp]

    public void Setup() { }

    [TestCase(16000, 4789.8)]
    [TestCase(150_000, 39989.5)]
    [TestCase(10000, 2989.8)]
    [TestCase(160_0000, 474_989.5)]
    [TestCase(-800, 0)]

    public void CalculateTax_ReturnsExpectedTaxAmount(decimal income, decimal expectedTaxAmount)
      {
      List<TaxBracket> taxBrackets =
        [
        new TaxBracket()
          {
          LowerLimit = 0,
          UpperLimit =  50_000,  
          Rate = 10
          },
        new TaxBracket()
          {
          LowerLimit = 51,
          UpperLimit = 100_000,
          Rate = 20
          },
        new TaxBracket()
          {
      LowerLimit = 100_001,
          UpperLimit = decimal.MaxValue,
          Rate = 30
          },
        ];

      taxCalculatorService = new ProgressiveTaxCalculatorService(taxBrackets);
      decimal taxAmount = taxCalculatorService.CalculateTax(income);
      Assert.That(taxAmount, Is.EqualTo(expectedTaxAmount));
      }
    }
  }