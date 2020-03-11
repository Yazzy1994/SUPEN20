using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUPEN20DB.Seeder
{
    public class DataSeeder
    {
        public static void Initialize(SUPEN20DbContext context)
        {

            context.Database.EnsureCreated();

            if (!context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product {Title = "It's Funny Because It's True", Description = "A grey T-shirt made of ecologically sustainable wool. Slim fit.", Price = 200, ImgId = "Its_Funny_Because_Its_True"},
                    new Product {Title = "Think Outside The Box", Description = "A white T-shirt with beautiful print made of ecologically sustainable wool. Slim fit.", Price = 400, ImgId = "Think_outside_the_box"},
                    new Product {Title = "Change The World", Description = "UnicornsCanCode's original T-shirt. Made of ecologically sustainable wool. Slim fit.", Price = 300, ImgId = "Change_The_World"},
                    new Product {Title = "My Code Works", Description = "A grey T-shirt made of ecologically sustainable wool. Slim fit.", Price = 300, ImgId = "My_Code_Works"},
                    new Product {Title = "Code Blooded", Description = "A red T-shirt made of ecologically sustainable wool. Slim fit.", Price = 150, ImgId = "Code_Blooded"},
                    new Product {Title = "Coding Girl", Description = "A navy blue T-shirt with beautiful print made of ecologically sustainable wool. Slim fit.", Price = 200, ImgId = "Coding_Girl"},
                    new Product {Title = "Eat Code Sleep", Description = "Black shirt made of ecologically sustainable wool. Slim fit.", Price = 300, ImgId = "Eat_Code_Sleep"},
                    new Product {Title = "Unicorns Can Code", Description = "A white T-shirt made of ecologically sustainable wool. Slim fit.", Price = 300, ImgId = "Unicorns_can_code"}
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

                var orders = new List<Order>() {
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 123,Created = DateTime.UtcNow, LastModified= DateTime.UtcNow},
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 456, Created = DateTime.UtcNow, LastModified= DateTime.UtcNow},
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 789, Created = DateTime.UtcNow, LastModified= DateTime.UtcNow},
                    new Order {OrderId = Guid.NewGuid(), OrderNumber= 100,Created = DateTime.UtcNow, LastModified= DateTime.UtcNow}
                };

                context.Orders.AddRange(orders);

                context.SaveChanges();

            }

        }
    }

}

