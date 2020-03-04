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

        private HttpClient client = new HttpClient();

        public CartController()
        {
            client.BaseAddress = new Uri("https://localhost:44305/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // Tillfälligt: Hämtar och lägger till alla produkter från databasen i kundvagnen
        public IActionResult Cart()
        {
            if (CurrentCart.Products.Count() == 0)
            {
                HttpResponseMessage response = client.GetAsync("/api/product").Result;
                List<ProductModel> data = response.Content.ReadAsAsync<List<ProductModel>>().Result;
            
                foreach (var p in data)
                {
                    AddToCart(p);
                }
            }

            CurrentCart.Total = CartTotal(CurrentCart);

            return View(CurrentCart);
        }

        public IActionResult OrderStatus(Boolean status)
        {
            ViewBag.Message = status;

            return View();
        }

        // Lägger till en produkt i kundvagnen
        public void AddToCart(ProductModel product)
        {
            CurrentCart.Products.Add(product);
        }

        // Tar bort en produkt-typ ur kundvagnen
        public IActionResult RemoveItemFromCart(Guid product)
        {
            var p = CurrentCart.Products.Where(p => p.ProductId == product).FirstOrDefault();

            if (p != null)
            {
                CurrentCart.Products.Remove(p);
            }

            return RedirectToAction("Cart");
        }

        public IActionResult UpdateProductQuantity(Guid product, int quantity)
        {
            var p = CurrentCart.Products.Where(p => p.ProductId == product).FirstOrDefault();

            p.Quantity = quantity;

            return RedirectToAction("Cart");
        }

        // Räknar ut det totala priset för alla varor i kundvagnen
        public decimal CartTotal(CartModel cart)
        {
            decimal total = 0;
            foreach(var p in cart.Products)
            {
                var subtotal = p.Price * p.Quantity;
                total += subtotal;
            }
            return total;
        }
    }
}