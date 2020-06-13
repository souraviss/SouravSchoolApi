using DemoApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Dto
{
    public class CustomerDto
    {

        public int Id { get; set; }
        internal DemoContext Db { get; set; }
        MySqlConnection conn;
        public CustomerDto(DemoContext db)
        {
            this.Db = db;
            conn = this.Db.GetConnection();
        }

        public async Task<List<Customer>> ReadAllCustomer()
        {
            List<Customer> customers = new List<Customer>();
            conn.OpenAsync();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM customers", conn);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
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
            return customers;
        }

        public async Task<int> InsertAsync(Customer cust)
        {
            conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO customers(email,NAME,ACTIVE) VALUES(@email,@name,@active);";
            cmd.Parameters.AddWithValue("@email", cust.email);
            cmd.Parameters.AddWithValue("@name", cust.name);
            cmd.Parameters.AddWithValue("@active", cust.Active);

            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
            return Id;
        }

        public async Task<Customer> FindOneAsync(int Id)
        {
            Customer cust = new Customer();
            conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `customers` WHERE `id`=@id";
            cmd.Parameters.AddWithValue("@id", Id);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                   cust.Id = reader.GetInt32(0);
                   cust.name = reader.GetString(1);
                    cust.Active = Convert.ToBoolean(reader["ACTIVE"]);
                }
            }
            return cust;
        }

        public async Task<int> UpdateAsync(Customer cust,int Id)
        {
            conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE customers SET email=@email,NAME=@name,ACTIVE=@active WHERE id=@id;";
            cmd.Parameters.AddWithValue("@email", cust.email);
            cmd.Parameters.AddWithValue("@name", cust.name);
            cmd.Parameters.AddWithValue("@active", cust.Active);
            cmd.Parameters.AddWithValue("@id", Id);

            Id = await cmd.ExecuteNonQueryAsync();
            return Id;
        }

        public async Task DeleteAsync(int Id)
        {
            conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM customers WHERE id= @id;";
            cmd.Parameters.AddWithValue("@id", Id);
            await cmd.ExecuteNonQueryAsync();
        }

    }
}
