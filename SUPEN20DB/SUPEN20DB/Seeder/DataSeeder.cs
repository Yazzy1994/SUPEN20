using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUPEN20DB.Seeder
{
    public class DataSeeder
    {
        public static void Initialize(SUPEN20DbContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product { /*Id = 1,*/ Title = "Grey Hoodie", Description = "A grey hoodie", Price = 200},
                    new Product { /*Id = 2,*/ Title = "Blue Hoodie", Description = "A blue hoodie", Price = 400}
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            if (!context.Credits.Any())
            {
                var credits = new List<Credit>()
                {
                    new Credit { /* Id = 1 */ Amount = 20000, CustomerId = 1, LastModified = DateTime.Now }
                };
                context.Credits.AddRange(credits);
                context.SaveChanges();
            }
            if (!context.Orders.Any())
            {
                var orders = new List<Order>()
                {
                    new Order { /* Id = 1 */ OrderNumber = 11111, Total = 20000, Product = context.Products.FirstOrDefault(), ProductQuantity = 100, Created = DateTime.Now, LastModified = DateTime.Now, CustomerId = 1}
                };
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }

}

