using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SUPEN20DB.Seeders
{
    public class Seeder
    {
        private static IHostEnvironment _hosting; 
        public Seeder(IHostEnvironment hosting)
        {
            _hosting = hosting; 
        }

        public static void SeedData(SUPEN20DbContext _context)
        {
            _context.Database.EnsureCreated();
            
            if(!_context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product  { ProductId = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"), Title = "Grey Hoodie", Description = "A grey hoodie", Price = 300, ImgId = "hoodie" },


                   new Product  { ProductId = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), Title = "Blue Super Hoodie",Description = "A rare blue hoodie",Price = 500,ImgId = "hoodie" },



                  new Product {ProductId = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),  Title = "Unicorn Hoodie",Description = "A limited edition unicorn",Price = 500,  ImgId = "hoodie" }

                };
                _context.Products.AddRange(products);
               
                _context.SaveChanges(); 
            }

            if (!_context.Orders.Any())
            {

                var orders = new List<Order>() {
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 123,Created = DateTime.UtcNow, LastModified= DateTime.UtcNow},
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 456, Created = DateTime.UtcNow, LastModified= DateTime.UtcNow},
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 789, Created = DateTime.UtcNow, LastModified= DateTime.UtcNow},
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 100,Created = DateTime.UtcNow, LastModified= DateTime.UtcNow}
                };
              
                _context.Orders.AddRange(orders);

                _context.SaveChanges();

            }

          

            

            if (!_context.Credits.Any())
            {

                var credits = new List<Credit>()
               {
                   new Credit {CreditId = Guid.NewGuid(), Amount = 1000, CustomerId= 1, LastModified= DateTime.UtcNow },
                   new Credit {CreditId = Guid.NewGuid(), Amount = 500, CustomerId= 1, LastModified= DateTime.UtcNow },
                   new Credit {CreditId = Guid.NewGuid(), Amount = 500, CustomerId= 1, LastModified= DateTime.UtcNow },
                   new Credit {CreditId = Guid.NewGuid(), Amount = 500, CustomerId= 1, LastModified= DateTime.UtcNow }


               };

                _context.AddRange(credits); 
                _context.SaveChanges();

            }




        }
    }
}
