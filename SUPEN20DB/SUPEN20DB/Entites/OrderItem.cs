using System;
using System.Collections.Generic;
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
        public ICollection<Product> Products { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}