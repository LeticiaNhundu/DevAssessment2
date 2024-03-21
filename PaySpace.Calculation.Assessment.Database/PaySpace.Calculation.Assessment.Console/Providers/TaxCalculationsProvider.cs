using PaySpace.Calculation.Assessment.Console.Models;
using System.Data;
using System.Data.SqlClient;

namespace PaySpace.Calculation.Assessment.Console.Providers
  {
  public class TaxCalculationsProvider
    {
    private string _connectionString;

    public TaxCalculationsProvider(string connectionString)
      {
      _connectionString = connectionString;
      }
    public void UpdateTaxCalculation(List<TaxUpdate> updateParameters)
      {
      using (SqlConnection connection = new SqlConnection(_connectionString))
        {
        connection.Open();

        string updateQuery = "UPDATE TaxCalculation SET CalculatedTax = @Tax, NetPay = @NetPay WHERE PkTaxCalculationId = @CalculationId";
        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
        foreach (var parameter in updateParameters)
          {
          var taxParameter = new SqlParameter("@Tax", SqlDbType.Decimal);
          taxParameter.Value = parameter.Tax;
          updateCommand.Parameters.Add(taxParameter);

          var netPayParameter = new SqlParameter("@NetPay", SqlDbType.Decimal);
          netPayParameter.Value = parameter.NetPay;
          updateCommand.Parameters.Add(netPayParameter);

          var calculationIdParameter = new SqlParameter("@CalculationId", SqlDbType.Int);
          calculationIdParameter.Value = parameter.CalculationId;
          updateCommand.Parameters.Add(calculationIdParameter);

          updateCommand.ExecuteNonQuery();
          updateCommand.Parameters.Clear();
          }
        }
      }
    public List<TaxRetrieve> GetTaxCalculations()
      {
      List<TaxRetrieve> taxCalculations = new List<TaxRetrieve>();

      string taxCalculationQuery = "SELECT Income,FKCountryId,PkTaxCalculationId FROM TaxCalculation";
      using (SqlConnection connection = new SqlConnection(_connectionString))
        {
        SqlCommand command = new SqlCommand(taxCalculationQuery, connection);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
          {
          TaxRetrieve taxCalculation = new TaxRetrieve()
            {
            Income = (decimal)reader["Income"],
            CountryId = (int)reader["FKCountryId"],
            CalculationId = (int)reader["PkTaxCalculationId"]
            };

          taxCalculations.Add(taxCalculation);
          }
        reader.Close();
        
      }
        return taxCalculations;
      }
    }
  }
