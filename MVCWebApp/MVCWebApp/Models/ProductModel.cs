using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWebApp.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        public string ImgId { get; set; }

        

    }
}