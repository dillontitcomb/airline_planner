using System;
using MySql.Data.MySqlClient;
using AirlinePlannerApp;

namespace AirlinePlannerApp.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
