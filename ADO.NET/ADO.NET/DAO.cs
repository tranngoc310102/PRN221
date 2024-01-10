using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWPF_DAO.DAO
{
    public class DAO
    {
        public static SqlConnection GetConnection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("tungc#.json").Build();
            string ConnectionStr = config.GetConnectionString("DB");
            return new SqlConnection(ConnectionStr);
        }

        public static DataTable GetDataBySQl(string sql, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(sql, GetConnection());
            if (parameters != null || parameters.Length == 0) command.Parameters.AddRange(parameters);

            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = command;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static int ExecuteSql(String sql, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(sql, GetConnection());
            if (parameters != null || parameters.Length == 0)
                command.Parameters.AddRange(parameters);

            command.Connection.Open();
            int count = command.ExecuteNonQuery();

            command.Connection.Close();

            return count;
        }
    }
}