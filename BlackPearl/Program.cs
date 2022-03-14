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
            
            using (var db = new PearlDbContext(_optionsBuilder.Options))
                foreach (var m in db.PearlLists)
                {
                    Console.WriteLine($"KR: {m.totalPrice} ID:({m.totalPrice})");
                }
            
            

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
                // Häs ska kod in 

                // eller skapa halsband
                //Create some customers
                //var PearlList_List = new List<PearlList>(); // en lista av halsband
                //for (int i = 0; i < 5; i++)
                //{
                //    PearlList_List.Add(new PearlList());
                //}
                ////Create some orders randomly linked to customers
                //var rnd = new Random();
                //int prnd = rnd.Next(1, 5);

                //var ListOfOrders = new List<Pearl>();
                //for (int i = 0; i < prnd; i++)
                //{
                //    ListOfOrders.Add(new Pearl(PearlList_List[rnd.Next(0, PearlList_List.Count)].PearlListID));
                //}

                ////Add it to the Database
                //PearlList_List.ForEach(n => db.PearlLists.Add(n));
                //ListOfOrders.ForEach(p => db.Pearls.Add(p));

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








