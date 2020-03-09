using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemAPI.Models;

namespace SystemAPI.Profiles
{
    public class OrderDto
    {
        public Guid OrderId {get; set; }
        public int OrderNumber { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public string CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Created { get; set; }
    }
}
