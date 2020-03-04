using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class OrderItemController : Controller
    {
        private HttpClient client = new HttpClient();
        private List<OrderItemModel> item = new List<OrderItemModel>(); 

        public OrderItemController()
        {
            client.BaseAddress = new Uri("http://localhost:51044/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult AddToCart(Guid productId)
        {
            /*var productModel = new ProductModel()*//*;*/

            HttpResponseMessage response = client.GetAsync("/api/orders").Result;
            List<OrderItemModel> data = response.Content.ReadAsAsync<List<OrderItemModel>>().Result;
       
            if (data == null)
            {
                item = new List<OrderItemModel>();
                item.Add(new OrderItemModel
                {

                    ProductId = productId,
                    Quantity = 1

                });

                //Nåt måste läggas till här så den sparas i carten 
            }
            else
            {
                int index = Exists(item, productId);
                if (index == -1)
                {
                    item.Add(new OrderItemModel
                    {
                        ProductId = productId,
                        Quantity = 1
                    });
                }
                else
                {
                    item[index].Quantity++;
                }

                //Nåt som sparar detta i carten.
            }

            return View(data);
        }

        //public IActionResult AddToCart(Guid productId)
        //{
            

        //}

        private int Exists(List<OrderItemModel> cart, Guid id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }


    }
}