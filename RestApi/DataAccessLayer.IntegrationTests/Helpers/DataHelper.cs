using DataAccessLayer.IntegrationTests.Common;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Helpers
{
    internal static class DataHelper
    {
        public static void DeleteAllFromDatabase()
        {
            using var connection = new SqlConnection(ConnectionService.GetConnectionString());
            connection.Open();

            var tableName = "Products";

            var sqlExpression = $"DELETE FROM {tableName}";

            var productCommand = new SqlCommand(sqlExpression, connection);
            productCommand.ExecuteNonQuery();

            tableName = "ProductTypes";

            sqlExpression = $"DELETE FROM {tableName}";

            var productTypeCommand = new SqlCommand(sqlExpression, connection);
            productTypeCommand.ExecuteNonQuery();

            tableName = "Brands";

            sqlExpression = $"DELETE FROM {tableName}";

            var brandCommand = new SqlCommand(sqlExpression, connection);
            brandCommand.ExecuteNonQuery();

            tableName = "Towns";

            sqlExpression = $"DELETE FROM {tableName}";

            var townCommand = new SqlCommand(sqlExpression, connection);
            townCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}
