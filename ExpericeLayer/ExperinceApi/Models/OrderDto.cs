using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperinceApi.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public int OrderNumber { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string CustomerId { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Approved,
        Denied
    }
}
