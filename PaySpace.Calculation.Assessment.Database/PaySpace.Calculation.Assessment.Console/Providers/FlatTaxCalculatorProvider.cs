using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;
using System.Data.SqlClient;

namespace PaySpace.Calculation.Assessment.Console.Providers
  {
  public class FlatTaxCalculatorProvider : ITaxCalculatorProvider
    {
    private readonly string _connectionString;
    public FlatTaxCalculatorProvider(string connectionString)
      {
      _connectionString = connectionString;
      }
    public ITaxCalculatorService GetTaxCalculatorService(int countryId)
      {
      decimal flatRate = 0;
      decimal minimumThreshold = 0;
      using (SqlConnection connection = new SqlConnection(_connectionString))
        {
        string queryString = "SELECT RateCode, Rate FROM TaxRate WHERE fkCountryId = @CountryId AND (RateCode = 'FLATRATE' OR RateCode = 'THRES')";
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@CountryId", countryId);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
          {
          while (reader.Read())
            {
            if (reader["RateCode"].ToString() == "FLATRATE")
              {
              flatRate = decimal.Parse(reader["Rate"].ToString());
              }
            else if (reader["RateCode"].ToString() == "THRES")
              {
              minimumThreshold = decimal.Parse(reader["Rate"].ToString());
              }
            }

          }
        finally
          {
          reader.Close();
          }
        }

      return new FlatTaxCalculatorService(flatRate, minimumThreshold);
      }
    }
  }
