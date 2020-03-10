using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemAPI.Models;
using SystemAPI.Services;

namespace SystemAPI.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId}/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRespository<Order> _orderRespository;
        public OrderItemsController(
            IRespository<Order> orderRespository, IMapper mapper)
        {
            _orderRespository = orderRespository;
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<ActionResult<OrderItem>> GetOrderItem(Guid orderId)
        {
            try
            {
                if (!_orderRespository.Exist(orderId))
                {
                    return NotFound();
                }

                var orderFromRepo = await _orderRespository.GetByIdAsync(orderId);
                var orderitemDtoList = _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDto>>(orderFromRepo.OrderItems);

                return Ok(orderitemDtoList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("{orderItemId}")]
        public async ValueTask<ActionResult<OrderItem>> GetOrderItemById(Guid orderId, Guid orderItemId)
        {
            try
            {
                if (!_orderRespository.Exist(orderId))
                {
                    return NotFound();
                }

                var order = await _orderRespository.GetByIdAsync(orderId);
                var item = order.OrderItems.Where(i => i.OrderItemId == orderItemId).FirstOrDefault();
                var itemDto = _mapper.Map<OrderItem, OrderItemDto>(item);

                return Ok(itemDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }
    }
}