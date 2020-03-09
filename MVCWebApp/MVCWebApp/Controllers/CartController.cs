﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

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
            client.BaseAddress = new Uri("http://localhost:51044");
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

        [Authorize]
        public IActionResult CreateOrder()
        {
            var orderId = Guid.NewGuid();

            List<OrderItemModel> orderItems = new List<OrderItemModel>();
            foreach (var product in CurrentCart.Products)
            {
                var items = new OrderItemModel()
                {
                    OrderItemId = Guid.NewGuid(),
                    ProductId = product.ProductId,
                    ProductDescription = product.Description,
                    ProductPrice = product.Price,
                    Quantity = product.Quantity,
                    Total = CurrentCart.Total
                };
                orderItems.Add(items);
            }

            var order = new OrderModel()
            {
                OrderId = orderId,
                OrderNumber = 123,
                Created = DateTime.Now,
                OrderStatus = 0,
                OrderItems = orderItems,
                CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier) // will give the user's userId
            };

            HttpResponseMessage response = client.PostAsJsonAsync("/api/orders/", order).Result;
            response.Content.ReadAsStringAsync();

            return RedirectToAction("Cart");
        }

        // Lägger till en produkt i kundvagnen
        public IActionResult AddProductToCart(ProductModel product)
        {
            var id = product.ProductId; //Ifall samma productId läggs till i kundvagn så kör foreach för att loopa igenom alla produkter somm läggs i kundvagnen. Om productId redan finns läggs till så ökas det kvantitet istället för att lägga till samma produkt flera gånger.

            foreach (var p in CurrentCart.Products)
            {
                if (p.ProductId == id)
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
            foreach (var p in cart.Products)
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