using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUPEN20DB.Entites
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImgId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
        public DateTime Created { get; set; } = DateTime.Now;
    }
}