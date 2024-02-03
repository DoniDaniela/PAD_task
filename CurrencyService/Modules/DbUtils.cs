using System.Data.SqlClient;

namespace CurrencyService.Modules
{
    public static class DbUtils
    {

        public static void InitDB(string ConnStr)
        {
            if (!string.IsNullOrWhiteSpace(ConnStr))
            {
                using (SqlConnection con = new SqlConnection(ConnStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(@"IF DB_ID('Currency') IS NULL CREATE DATABASE Currency;", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                using (SqlConnection con = new SqlConnection(ConnStr.Replace("master", "Currency")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(@"IF OBJECT_ID('Logs', 'U') IS NULL CREATE TABLE Logs (Request varchar(1000),StatusCode int,Response nvarchar(max));", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
        }

        public static void SetLog(string ConnStr, string? Request, int? Status, string? Response)
        {
            if (!string.IsNullOrWhiteSpace(ConnStr))
                using (SqlConnection con = new SqlConnection(ConnStr.Replace("master", "Currency")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [Logs] ([Request], [StatusCode], [Response]) VALUES (@pRequest, @pStatusCode, @pResponse)", con))
                    {
                        cmd.Parameters.AddWithValue("@pRequest", Request ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@pStatusCode", Status ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@pResponse", Response ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
        }

    }
}
