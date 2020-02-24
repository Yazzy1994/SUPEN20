using System;
using System.Collections.Generic;
using System.Text;

namespace SUPEN20DB.Entites
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgId { get; set; }


    }
}
