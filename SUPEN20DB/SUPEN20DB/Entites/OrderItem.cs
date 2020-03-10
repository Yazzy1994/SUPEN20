using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUPEN20DB.Entites
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //This is the join table for Products and Orders 
                                                               //In EF Core it is necessary to include an entity in the model to represent the join table. The EF Team are planning on removing the need for a join entity at some point.
        public Guid OrderItemId { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid OrderId { get; set; }

        public Order Order { get; set; } 
        public DateTime Created { get; set; } = DateTime.Now;
    }
}