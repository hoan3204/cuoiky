using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoanchinh
{
    public class DatabaseHelper
    {
        private static string connectionString = "Server=HOAN;Database=QuanLyDoAnNhanh;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
