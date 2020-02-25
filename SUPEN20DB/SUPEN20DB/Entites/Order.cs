using System;
using System.Collections.Generic;
using System.Text;

namespace SUPEN20DB.Entites
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int OrderNumber {get; set;}
        public decimal Total { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int CustomerId { get; set; }

    }
}
