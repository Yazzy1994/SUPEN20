using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUPEN20DB.Entites
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderItemId { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid OrderId { get; set; }

        public Order Order { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}