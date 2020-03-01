using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUPEN20DB.Entites
{
    public enum OrderStatus
    {
        Pending,
        Approved,
        Denied
    }

    public class Order
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }

}