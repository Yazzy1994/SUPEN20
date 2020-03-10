using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models
{
    public class OrderItemModel //As the same structure as the OrderItemDto in the SystenLayer. 
    {
        public Guid OrderItemId { get; set; }  

        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
       
    }
}
