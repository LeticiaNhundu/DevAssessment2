using NUnit.Framework;
using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;

namespace PaySpace.Calculation.Assessment.Test.Service {
  [TestFixture]
  public class FlatTaxCalculatorTest{
  private FlatTaxCalculatorService? taxCalculatorService;

    [SetUp]
    public void Setup() {}

    [TestCase(5000, 0)]
    [TestCase(16000, 20000)]
    [TestCase(150000, 20000)]
    [TestCase(10000, 0)]
    [TestCase(1600000, 20000)]
    [TestCase(-800, 0)]
    public void CalculateTax_ReturnsExpectedTaxAmount(decimal income, decimal expectedTaxAmount)
      {
      decimal threshold = 20000;
      decimal flatRate = 15000;
      taxCalculatorService = new FlatTaxCalculatorService(threshold, flatRate);
      decimal taxAmount = taxCalculatorService.CalculateTax(income);
      Assert.That(taxAmount, Is.EqualTo(expectedTaxAmount));
      }
    }
  }