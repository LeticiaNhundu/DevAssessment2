
using PaySpace.Calculation.Assessment.Console.Models;
using PaySpace.Calculation.Assessment.Console.Services.TaxCalculatorService;
using System.Data.SqlClient;

namespace PaySpace.Calculation.Assessment.Console.Providers
  {
  public class ProgressiveTaxCalculatorProvider : ITaxCalculatorProvider
    {
    private readonly string _connectionString;
    public ProgressiveTaxCalculatorProvider(string connectionString)
      {
      _connectionString = connectionString;
      }
    public ITaxCalculatorService GetTaxCalculatorService(int countryId)
      {
      List<TaxBracket> taxBrackets = new List<TaxBracket>();

      using (SqlConnection connection = new SqlConnection(_connectionString))
        {
        string queryString = "SELECT l.LowerLimit, l.UpperLimit, l.Rate FROM TaxBracketLine l INNER JOIN TaxBracket b ON l.FkTaxBracketId = b.PkTaxBracketId WHERE fkCountryId = @CountryId ORDER BY OrderNumber";
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@CountryId", countryId);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
          {
          while (reader.Read())
            {
            var taxBracket = new TaxBracket
              {
              LowerLimit = decimal.Parse(reader["LowerLimit"].ToString()),
              UpperLimit = decimal.Parse(reader["UpperLimit"].ToString()),
              Rate = decimal.Parse(reader["Rate"].ToString())
              };
            taxBrackets.Add(taxBracket);
            }
          }
        finally
          {
          reader.Close();
          }
        }
      return new ProgressiveTaxCalculatorService(taxBrackets);
      }
    }
  }
