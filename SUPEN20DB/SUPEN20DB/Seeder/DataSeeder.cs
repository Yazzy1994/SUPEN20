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
                    new Product { /*Id = 1,*/ Title = "It's Funny Because It's True", Description = "A grey T-shirt made of ecologically sustainable wool. Slim fit.", Price = 200, ImgId = "Its_Funny_Because_Its_True"},
                    new Product { /*Id = 2,*/ Title = "Think Outside The Box", Description = "A white T-shirt with beautiful print made of ecologically sustainable wool. Slim fit.", Price = 400, ImgId = "Think_outside_the_box"},
                    new Product { /*Id = 1,*/ Title = "Change The World", Description = "UnicornsCanCode's original T-shirt. Made of ecologically sustainable wool. Slim fit.", Price = 300, ImgId = "Change_The_World"},
                    new Product { /*Id = 2,*/ Title = "My Code Works", Description = "A grey T-shirt made of ecologically sustainable wool. Slim fit.", Price = 300, ImgId = "My_Code_Works"}
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            if (!context.Credits.Any())
            {
                var credits = new List<Credit>()
                {
                    new Credit { /* Id = 1 */ Amount = 20000, CustomerId = 1, LastModified = DateTime.Now },
                    new Credit { /* Id = 1 */ Amount = 50000, CustomerId = 2, LastModified = DateTime.Now }
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

