using DemoApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp
{
    public class DemoContext
    {
        public string ConnectionString { get; set; }
        public DemoContext(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM customers", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            email = reader["email"].ToString(),
                            name = reader["NAME"].ToString(),
                            Active = Convert.ToBoolean(reader["ACTIVE"])
                        });
                    }
                }
            }
            return customers;
        }
       
    }
}
