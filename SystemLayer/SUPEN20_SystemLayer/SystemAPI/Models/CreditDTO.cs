using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemAPI.Models
{
    public class CreditDTO
    {
        public Guid CreditId { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
    }
}
