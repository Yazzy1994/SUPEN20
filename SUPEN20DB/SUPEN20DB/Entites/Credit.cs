using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SUPEN20DB.Entites
{
    public class Credit
    {
        public Guid CreditId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public DateTime LastModified { get; set; }

    }
}
