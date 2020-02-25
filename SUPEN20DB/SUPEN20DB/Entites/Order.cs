using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SUPEN20DB.Entites
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int OrderNumber {get; set;}
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

    }
}
