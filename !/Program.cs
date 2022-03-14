using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

using SeidoDemoDb;
using SeidoDemoModels;

namespace SeidoApplication
{
    class Program
    {
        private static DbContextOptionsBuilder<SeidoDemoDbContext> _optionsBuilder;
        static void Main(string[] args)
        {
            BuildOptions();

            SeedDataBase();
            QueryDatabaseAsync().Wait();

            //int max = MyList.Max();

            //double?[] doubles = { null, 1.5E+104, 9E+103, -2E+103 };

            //double? max = doubles.Max();

            //Console.WriteLine("The largest number is {0}.", max);

            /*
             This code produces the following output:

             The largest number is 1.5E+104.
            */
            
            using (var db = new SeidoDemoDbContext(_optionsBuilder.Options))
                foreach (var m in db.Customers)
            {
                Console.WriteLine($"KR: {m.totalPrice} ID:({m.Comment})");
            }
            
            //var values = new List<int> { 2, 9, 1, 3 };
            //Console.WriteLine(values.Max()); // Output: 9

            //var otherValues = new List<int?> { 2, 9, 1, 3, null };
            //Console.WriteLine(otherValues.Max()); // Output: 9
        }

        private static void BuildOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder<SeidoDemoDbContext>();

            #region Ensuring appsettings.json is in the right location
            Console.WriteLine($"DbConnections Directory: {DBConnection.DbConnectionsDirectory}");

            var connectionString = DBConnection.ConfigurationRoot.GetConnectionString("SQLite_pearlv2");
            if (!string.IsNullOrEmpty(connectionString))
                Console.WriteLine($"Connection string to Database: {connectionString}");
            else
            {
                Console.WriteLine($"Please copy the 'DbConnections.json' to this location");
                return;
            }
            #endregion

            _optionsBuilder.UseSqlite(connectionString);
        }

        private static void SeedDataBase()
        {
            using (var db = new SeidoDemoDbContext(_optionsBuilder.Options))
            {
                // eller skapa halsband
                //Create some customers
                var CustomerList = new List<Customer>(); // en lista av kunder/halsband
                for (int i = 0; i < 5; i++)
                {
                    CustomerList.Add(new Customer());
                }
                //Create some orders randomly linked to customers
                var rnd = new Random();

                int prnd = rnd.Next(1, 5);

                var OrderList = new List<Order>();
                for (int i = 0; i < prnd; i++)
                {
                    OrderList.Add(new Order(CustomerList[rnd.Next(0, CustomerList.Count)].CustomerID));
                }

                //Add it to the Database
                CustomerList.ForEach(cust => db.Customers.Add(cust));
                OrderList.ForEach(order => db.Orders.Add(order));

                db.SaveChanges();
            }
        }
        private static async Task QueryDatabaseAsync()
        {
            using (var db = new SeidoDemoDbContext(_optionsBuilder.Options))
            {
                var customers = await db.Customers.CountAsync();
                var orders = await db.Orders.CountAsync();

                Console.WriteLine($"Nr of Customers: {customers}");
                Console.WriteLine($"Nr of Orders: {orders}");
            }
        }
    }
}
