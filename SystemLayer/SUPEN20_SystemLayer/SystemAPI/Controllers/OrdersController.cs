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
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _orderRespository;

        public OrdersController(IRepository<Order> orderRespository, IMapper mapper)
        {
            _orderRespository = orderRespository;
            _mapper = mapper;
        }

        //GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                var ordersFromRepo = await _orderRespository.GetAllAsync();
                var orderDtoList = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(ordersFromRepo);

                return Ok(orderDtoList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }

        }

        //GET: api/orders/{orderId}
        [HttpGet("{orderId}")]
        public async ValueTask<ActionResult<Order>> GetOrders(Guid orderId)
        {
            try
            {
                if (!_orderRespository.Exist(orderId))
                {
                    return NotFound();
                }

                var orderFromRepo = await _orderRespository.GetByIdAsync(orderId);
                var orderDto = _mapper.Map<Order, OrderDto>(orderFromRepo);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }

        }

        //POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody]OrderDto orderDto)
        {
            try
            {
                var validator = new SaveOrderValidator(); //Makes a instance of SaveOrderValidator
                var orderEntity = _mapper.Map<OrderDto, Order>(orderDto); //Maps from OrderDto to OrderEntity
                var validationResult = await validator.ValidateAsync(orderEntity); //Validates the orderentity 


                if (!validationResult.IsValid) //If it's not valid it returns errors. 
                {
                    return BadRequest(validationResult.Errors);
                }

                var orderFromRepo = _orderRespository.AddAsync(orderEntity); //Adds the new order in the database
                await _orderRespository.SaveChangesAsync();

                return CreatedAtAction("GetOrders", new { id = orderDto.OrderId }); //CreatedAtAction method returns HTTP 201 status code, if successful.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
            
        }

        //DELETE: api/orders/{orderId}
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid orderId)
        {
            try
            {
                var orderFromRepo = await _orderRespository.GetByIdAsync(orderId);

                if (orderFromRepo == null)
                {
                    return NotFound();
                }

                await _orderRespository.Delete(orderFromRepo);
                await _orderRespository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }

            return NoContent();
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
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}