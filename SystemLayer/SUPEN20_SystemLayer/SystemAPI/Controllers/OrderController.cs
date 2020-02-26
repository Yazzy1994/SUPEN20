using Microsoft.AspNetCore.Mvc;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemAPI.Services;


namespace SystemAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly SUPEN20DbContext _context;
        private readonly IRespository<Order> _orderRespository;

        public OrderController(SUPEN20DbContext context, IRespository<Order> orderRespository)
        {
            _context = context;
            _orderRespository = orderRespository;
        }
        //GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var ordersFromRepo = await _orderRespository.GetAllAsync();
            return Ok(ordersFromRepo);

        }
        //GET: api/orders/{orderId}
        [HttpGet("{orderId}")]
        public async ValueTask<ActionResult<Order>> GetOrders(Guid orderId)
        {
            var orderFromRepo= await _orderRespository.GetByIdAsync(orderId);
            return Ok(orderFromRepo); 
        }

        //POST: api/orders
        [HttpPost] 
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var orderFromRepo = _orderRespository.AddAsync(order);
            await _orderRespository.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = order.OrderId }, order); //CreatedAtAction method returns HTTP 201 status code, if successful. 
        }
      

        [HttpPut("{orderId}")]
        public async Task<ActionResult<Order>> UpdateOrder(Guid orderId, [FromBody] Order order)
        {
           try
            {
                if (orderId != order.OrderId)
                {
                    return BadRequest();
                }

                await _orderRespository.Update(order);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return NoContent(); 
        }

        //DELETE: api/orders/{orderId}
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid orderId)
        {
            var orderFromRepo = await _orderRespository.GetByIdAsync(orderId);
            await _orderRespository.Delete(orderFromRepo);
            await _orderRespository.SaveChangesAsync();

            return orderFromRepo;

        }

    }
}
