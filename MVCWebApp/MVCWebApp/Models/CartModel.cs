using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
    }
}
