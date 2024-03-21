using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaySpace.Calculation.Assessment.Console;
using PaySpace.Calculation.Assessment.Console.Models;
using PaySpace.Calculation.Assessment.Console.Providers;
using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;
using System.Diagnostics;

Console.WriteLine("***********************************************");
Console.WriteLine("*                   Welcome                   *");
Console.WriteLine("***********************************************");
Console.WriteLine("Calculating....");

IConfiguration configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");


using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
  {
  var app = services.GetRequiredService<App>();
  app.Run(args);
  }
catch (Exception e)
  {
  Console.WriteLine(e.Message);
  }

try
  {
  Stopwatch sw = Stopwatch.StartNew();

  List<TaxRetrieve> taxCalculations = GetTaxCalculations(connectionString);
  List<TaxUpdate> updateParameters = CalculateTaxes(taxCalculations, connectionString, services);
  UpdateTaxCalculations(updateParameters, connectionString);

  sw.Stop();
  Console.WriteLine($"{updateParameters.Count} calculations completed in {sw.ElapsedMilliseconds} ms");
  }
catch (Exception ex)
  {
  Console.WriteLine($"An error occurred: {ex.Message}");
  }

IHostBuilder CreateHostBuilder(string[] strings)
  {
  return Host.CreateDefaultBuilder()
      .ConfigureServices((hostContext, services) =>
      {

        services.AddTransient<ITaxCalculatorService, FlatTaxCalculatorService>();
        services.AddTransient<ITaxCalculatorService>(provider =>
              new FlatTaxCalculatorService(20000, 150000));
        services.AddTransient<App>();
      });
  }

List<TaxRetrieve> GetTaxCalculations(string connectionString)
  {
  TaxCalculationsProvider taxCalculationsProvider = new TaxCalculationsProvider(connectionString);
  return taxCalculationsProvider.GetTaxCalculations();
  }

List<TaxUpdate> CalculateTaxes(List<TaxRetrieve> taxCalculations, string connectionString, IServiceProvider services)
  {
  List<TaxUpdate> updateParameters = new List<TaxUpdate>();
  CountryProvider countryProvider = new CountryProvider(connectionString);
  ITaxCalculatorService flatTaxCalculatorService = services.GetRequiredService<ITaxCalculatorService>();

  foreach (var taxCalculation in taxCalculations)
    {
    int countryId = taxCalculation.CountryId;
    string taxMethod = countryProvider.GetTaxMethod(countryId);
    decimal income = taxCalculation.Income;
    decimal tax = 0m;
    decimal netPay = 0m;

    switch (taxMethod)
      {
      case "PROG":
        ITaxCalculatorProvider providerProg = new ProgressiveTaxCalculatorProvider(connectionString);
        var serviceProg = providerProg.GetTaxCalculatorService(countryId);
        tax = serviceProg.CalculateTax(income);
        netPay = income - tax;
        break;

      case "PERC":
        ITaxCalculatorProvider providerPerc = new PercentageTaxCalculatorProvider(connectionString);
        var servicePer = providerPerc.GetTaxCalculatorService(countryId);
        tax = servicePer.CalculateTax(income);
        netPay = income - tax;
        break;

      case "FLAT":
        ITaxCalculatorProvider providerFlat = new FlatTaxCalculatorProvider(connectionString);
        var serviceFlat = providerFlat.GetTaxCalculatorService(countryId);
        //   tax = flatTaxCalculatorService.CalculateTax(income);
        tax = serviceFlat.CalculateTax(income);
        netPay = income - tax;
        break;
      }

    TaxUpdate taxUpdate = new TaxUpdate()
      {
      Tax = tax,
      NetPay = netPay,
      CalculationId = taxCalculation.CalculationId
      };

    updateParameters.Add(taxUpdate);
    }

  return updateParameters;
  }

void UpdateTaxCalculations(List<TaxUpdate> updateParameters, string connectionString)
  {
  TaxCalculationsProvider taxCalculationsProvider = new TaxCalculationsProvider(connectionString);
  taxCalculationsProvider.UpdateTaxCalculation(updateParameters);
  }
