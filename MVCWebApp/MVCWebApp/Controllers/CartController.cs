using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class CartController : Controller
    {
        public static CartModel CurrentCart { get; set; } = new CartModel();
        private readonly IMapper _mapper;

        private HttpClient client = new HttpClient();

        public CartController(IMapper mapper)
        {
            _mapper = mapper; 
            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // Tillfälligt: Hämtar och lägger till alla produkter från databasen i kundvagnen
        public IActionResult Cart()
        {
            CurrentCart.Total = CartTotal(CurrentCart);

            return View(CurrentCart);
        }

        public IActionResult OrderStatus(Boolean status)
        {
            ViewBag.Message = status;

            return View();
        }


        // Lägger till en produkt i kundvagnen
        public IActionResult AddProductToCart(ProductModel product)
        {
            var id = product.ProductId; //Ifall samma productId läggs till i kundvagn så kör foreach för att loopa igenom alla produkter somm läggs i kundvagnen. Om productId redan finns läggs till så ökas det kvantitet istället för att lägga till samma produkt flera gånger. 

            
            foreach(var p in CurrentCart.Products)
            {
                if(p.ProductId == id)
                {
                    p.Quantity += 1;
                    return RedirectToAction("Index", "Home");
                }
                
            }

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

        // Calculates the total amouont of items in the cart
        // Used for the item count in the meny icon for the cart
        public static int ItemAmountInCart()
        {
            var amount = 0;
            if (CurrentCart.Products.Count() != 0)
            {
                foreach (var p in CurrentCart.Products)
                {
                    amount += p.Quantity;
                }
            }
            return amount;
        }
    }
}