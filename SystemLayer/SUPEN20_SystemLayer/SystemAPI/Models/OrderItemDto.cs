using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemAPI.Models
{
    public class OrderItemDto
    {
        public Guid OrderItemId { get; set; }


        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

    
    }
}