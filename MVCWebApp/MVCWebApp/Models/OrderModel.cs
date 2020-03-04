using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }
        public int OrderNumber { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Created { get; set; } = DateTime.Now; 
    }
}
