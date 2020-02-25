using System;
using System.Collections.Generic;
using System.Text;


namespace SUPEN20DB.Entites
{
    public class Credit
    {
        public Guid CreditId { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public DateTime LastModified { get; set; }

    }
}
