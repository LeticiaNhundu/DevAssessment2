using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PaySpace.Calculation.Assessment.Test.Service
  {
  [TestFixture]
  public class PercentageTaxServiceTest
    {
    private PercentageTaxCalculatorService? taxCalculatorService;

    [SetUp]
    public void Setup() { }

    [TestCase(5000, 1500)]
    [TestCase(16000, 4800)]
    [TestCase(-800, 0)]
    public void CalculateTax_ReturnsExpectedTaxAmount(decimal income, decimal expectedTaxAmount)
      {
      decimal percentage = 30;
      taxCalculatorService = new PercentageTaxCalculatorService(percentage);
      decimal taxAmount = taxCalculatorService.CalculateTax(income);
      Assert.That(taxAmount, Is.EqualTo(expectedTaxAmount));
      }
    }
  }