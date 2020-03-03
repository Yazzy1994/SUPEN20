using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class CartController : Controller
    {
        public static CartModel CurrentCart { get; set; } = new CartModel();
        private static List<ProductModel> Products { get; set; } = new List<ProductModel>();

        private HttpClient client = new HttpClient();

        public CartController()
        {
            client.BaseAddress = new Uri("https://localhost:44305/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //InitalizeCart();
        }
        public IActionResult Cart()
        {
            HttpResponseMessage response = client.GetAsync("/api/product").Result;
            List<ProductModel> data = response.Content.ReadAsAsync<List<ProductModel>>().Result;
            foreach (var p in data)
            {
                AddToCart(p);
            }

            CurrentCart.Total = CartTotal(CurrentCart);

            return View(CurrentCart);
        }

        //public void InitalizeCart()
        //{
        //    HttpResponseMessage response = client.GetAsync("/api/product").Result;
        //    List<ProductModel> data = response.Content.ReadAsAsync<List<ProductModel>>().Result;
        //    foreach (var p in data)
        //    {
        //        AddToCart(p);
        //    }

        //    CurrentCart.Total = CartTotal(CurrentCart);
        //}

        public static void AddToCart(ProductModel product)
        {
            Products.Add(product);
            CurrentCart.Products = Products;
        }

        public static decimal CartTotal(CartModel cart)
        {
            decimal total = 0;
            foreach(var p in cart.Products)
            {
                total += p.Price;
            }
            return total;
        }
    }
}