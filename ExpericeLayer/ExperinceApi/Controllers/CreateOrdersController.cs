using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExperinceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExperinceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateOrdersController : ControllerBase
    {
        public CreateOrdersController()
        {

        }

        [HttpPost]
        public void CreateOrders(ICollection<OrderItemDto> orderItems, string customerId)
        {
            // Skapar en order med ett specifikt order Id
            var order = new OrderDto();
            var orderId = Guid.NewGuid();
            order.OrderId = orderId;

            // Validerar att ett customerId har skickats med och att listan med orderItems inte är tom
            if(customerId == null || orderItems == null)
            {
                order.CustomerId = customerId;

                // Lägger till alla orderItems i ordern
                foreach (var i in orderItems)
                {
                    i.OrderId = orderId;
                    order.OrderItems.Add(i);
                }
            }
           
            /*  Kvar:
             *  Skapa en connection till vår Logic App.
             *  Skicka ordern till Logic App.
             *  Returnera svar ifall beställningen gått igenom eller inte.
             *  
             *  APIn ska vara en async Task<IActionResult<OrderItemDto>>
             */

        }
    }
}