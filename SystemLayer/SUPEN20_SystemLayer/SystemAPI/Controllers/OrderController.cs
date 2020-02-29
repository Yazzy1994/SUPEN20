using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemAPI.Profiles;
using SystemAPI.Services;
using SystemAPI.Validators;

namespace SystemAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRespository<Order> _orderRespository;

        public OrderController(IRespository<Order> orderRespository, IMapper mapper)
        {
            _orderRespository = orderRespository;
            _mapper = mapper;
        }

        //POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody]Order order)
        {
            var validator = new SaveOrderValidator();
            var validationResult = await validator.ValidateAsync(order);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var orderFromRepo = _orderRespository.AddAsync(order);
            await _orderRespository.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = order.OrderId },_mapper.Map<Order, OrderDto>(order)); //CreatedAtAction method returns HTTP 201 status code, if successful.
        }

        //DELETE: api/orders/{orderId}
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid orderId)
        {
            var orderFromRepo = await _orderRespository.GetByIdAsync(orderId);

            if (orderFromRepo == null)
            {
                return NotFound();
            }

            try
            {
                await _orderRespository.Delete(orderFromRepo);
                await _orderRespository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return NoContent();
        }

        //GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var ordersFromRepo = await _orderRespository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(ordersFromRepo));
        }

        //GET: api/orders/{orderId}
        [HttpGet("{orderId}")]
        public async ValueTask<ActionResult<Order>> GetOrders(Guid orderId)
        {
            if (!_orderRespository.Exist(orderId))
            {
                return NotFound();
            }

            var orderFromRepo = await _orderRespository.GetByIdAsync(orderId);
            return Ok(_mapper.Map<Order, OrderDto>(orderFromRepo));
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<Order>> UpdateOrder(Guid orderId, [FromBody] Order order)
        {
            try
            {
                if (!_orderRespository.Exist(orderId))
                {
                    return NotFound();
                }

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
    }
}