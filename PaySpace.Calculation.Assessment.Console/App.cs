using Microsoft.Extensions.Configuration;
using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;

namespace PaySpace.Calculation.Assessment.Console
  {

  public class App
    {
    private readonly ITaxCalculatorService _flatTaxCalculatorService;
    private readonly IConfiguration _configuration;

    public App(ITaxCalculatorService flatTaxCalculatorService, IConfiguration configuration)
      {
      _flatTaxCalculatorService = flatTaxCalculatorService;
      _configuration = configuration;
      }

    public void Run(string[] args)
      {


      }
    }
  }