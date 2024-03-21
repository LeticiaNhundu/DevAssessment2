using PaySpace.Calculation.Assessment.Console.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Providers
  {
  public class CountryProvider
    {
    private readonly string _connectionString;
    public CountryProvider(string connectionString)
      {
      _connectionString = connectionString;
      }
    public string GetTaxMethod(int countryId)
      {
      string taxMethod = string.Empty;

      using (SqlConnection connection = new SqlConnection(_connectionString))
        {
        string queryString = "SELECT TaxRegime FROM Country WHERE pkCountryId = @CountryId";
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@CountryId", countryId);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        try
          {
          if (reader.Read())
            {
            taxMethod = reader["TaxRegime"].ToString();
            }
          }
        finally
          {
          reader.Close();
          }

        }
      return taxMethod;
      }

    }
  }
