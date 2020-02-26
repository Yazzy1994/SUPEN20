using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemAPI.Profiles
{
    public class OrderDto
    {
        public int OrderNumnber { get; set; }
        public decimal Total { get; set; }
        public Guid ProductId { get; set; }
        public int Quantaty { get; set; }
    }
}
