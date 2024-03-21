using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;
using System.Data.SqlClient;

namespace PaySpace.Calculation.Assessment.Console.Providers
  {
  public class PercentageTaxCalculatorProvider : ITaxCalculatorProvider
    {
    private readonly string _connectionString;
    public PercentageTaxCalculatorProvider(string connectionString)
      {
      _connectionString = connectionString;
      }
    public ITaxCalculatorService GetTaxCalculatorService(int countryId)
      {
      decimal percentage = 0;
      using (SqlConnection connection = new SqlConnection(_connectionString))
        {
        string queryString = "SELECT Rate FROM TaxRate WHERE fkCountryId = @CountryId AND (RateCode = 'TAXPERC')";
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@CountryId", countryId);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
          {
          if (reader.Read())
            {
            percentage = decimal.Parse(reader["Rate"].ToString());
            }
          }
        finally
          {
          reader.Close();
          }
        }
      return new PercentageTaxCalculatorService(percentage);
      }
    }
  }
