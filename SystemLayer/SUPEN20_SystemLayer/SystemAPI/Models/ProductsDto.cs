using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SystemAPI.Models
{
    public class ProductsDto
    {
        public Guid ProductId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string ImgId { get; set; }
    }
}
