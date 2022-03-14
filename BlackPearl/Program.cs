using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using BlackPearl;
using SeidoDemoDb;


namespace SeidoApplication
{
    class Program
    {
        private static DbContextOptionsBuilder<PearlDbContext> _optionsBuilder;
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

            using (var db = new PearlDbContext(_optionsBuilder.Options))
                foreach (var m in db.PearlLists)
                {
                    Console.WriteLine($"KR: {m.totalPrice} ID:({m.totalPrice})");
                }

            //var values = new List<int> { 2, 9, 1, 3 };
            //Console.WriteLine(values.Max()); // Output: 9

            //var otherValues = new List<int?> { 2, 9, 1, 3, null };
            //Console.WriteLine(otherValues.Max()); // Output: 9













            //var myPearl1 = new Pearl();
            //var myPearl2 = new Pearl();

            //Console.WriteLine(myPearl1);
            //Console.WriteLine(myPearl2);

            //var myPearlNecklesList = new PearlList(5);

            // Här skapas en slumpmässig pärla
            Console.WriteLine("Factory skapade en slumpmässig pärla här:");
            Console.WriteLine(Pearl.Factory.CreateRandomPearl());

            // Listan/Halsbandet skapas här:
            var myPearlNecklesList = PearlList.Factory.CreateRandomList(35);

            // Här sorteras listan/halsbandet först efter diameter sedan färg(color) och sist form(shape)
            Console.WriteLine();
            myPearlNecklesList.Sort();
            Console.WriteLine("Alla pärlor sorterad efter diameter color shape (Halsbandet alltså)");
            Console.WriteLine(myPearlNecklesList);

            // Här räknas det ut hur många elemet det finns totalt i listan/halsbandet.
            var totalElements = myPearlNecklesList.Count();
            Console.WriteLine($"Totalt i halsbandet finns: {totalElements} element");

            // Här räknas det ut hur många Saltvattenpärlor det finns totalt i listan/halsbandet.
            var totalElementsSaltVatten = myPearlNecklesList.Count2();
            Console.WriteLine();
            Console.WriteLine($"Antal Saltvattenpärlor i halsbandet: {totalElementsSaltVatten}");

            // Här räknas det ut hur många Sötvattenpärlor det finns totalt i listan/halsbandet.
            var totalElementsSötVatten = myPearlNecklesList.Count3();
            Console.WriteLine();
            Console.WriteLine($"Antal Sötvattenpärlor i halsbandet: {totalElementsSötVatten}");

            // Hitta den första pärla med...... ex en vit, en med en diameter på 19mm, en som är Droppformad
            var findPearl = new Pearl();
            findPearl.Color = "Vit";
            //findPearl.Diameter = 19;
            //findPearl.Shape = "Droppformad";
            Console.WriteLine();
            Console.WriteLine($"Pärlan du söker efter finns på index: {myPearlNecklesList.IndexOf(findPearl)} i halsbandet");

            // Det totala priset för hela halsbandet.
            Console.WriteLine();
            Console.WriteLine($"Totala priset för hela halsbandet: {myPearlNecklesList.totalPrice} SEK");







        }// main

        private static void BuildOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder<PearlDbContext>();

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
            using (var db = new PearlDbContext(_optionsBuilder.Options))
            {
                // eller skapa halsband
                //Create some customers
                var CustomerList = new List<PearlList>(); // en lista av kunder/halsband
                for (int i = 0; i < 5; i++)
                {
                    CustomerList.Add(new PearlList());
                }
                //Create some orders randomly linked to customers
                var rnd = new Random();

                int prnd = rnd.Next(1, 5);

                var OrderList = new List<Pearl>();
                for (int i = 0; i < prnd; i++)
                {
                    //OrderList.Add(new Pearl(CustomerList[rnd.Next(0, PearlList.Count)].?????));
                }

                //Add it to the Database
                CustomerList.ForEach(cust => db.PearlLists.Add(cust));
                OrderList.ForEach(order => db.Pearls.Add(order));

                db.SaveChanges();
            }
        }
        private static async Task QueryDatabaseAsync()
        {
            using (var db = new PearlDbContext(_optionsBuilder.Options))
            {
                var pl = await db.PearlLists.CountAsync();
                var p = await db.Pearls.CountAsync();

                Console.WriteLine($"Nr of Customers: {pl}");
                Console.WriteLine($"Nr of Orders: {p}");
            }
        }
    }// class program
}








