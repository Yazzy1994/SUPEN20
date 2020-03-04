using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWebApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;


namespace MVCWebApp.Controllers
{

    public class CartController : Controller
    {
        public static CartModel CurrentCart { get; set; } = new CartModel();

        private HttpClient client = new HttpClient();

       

     

        // Tillfälligt: Hämtar och lägger till alla produkter från databasen i kundvagnen
        public IActionResult Cart()
        {

            return View(CurrentCart); 
        }
        
        public IActionResult AddProductToCart(ProductModel product)
        {
           CurrentCart.Products.Add(product);
            return RedirectToAction("Index", "Home");
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

        private int Exists(Guid id)
        {

            List<CartModel> cart = new List<CartModel>(); 
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Products.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public ProductModel Find(Guid products)
        {

            var list = new List<ProductModel>();
            return list.Find(p => p.ProductId == products);

        }


    }
}