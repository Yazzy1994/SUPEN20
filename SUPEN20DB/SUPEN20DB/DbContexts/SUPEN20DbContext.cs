using Microsoft.EntityFrameworkCore;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUPEN20DB.DbContexts
{
   public class SUPEN20DbContext : DbContext 
    {
        public SUPEN20DbContext(DbContextOptions<SUPEN20DbContext> options) : base(options) 
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Credit> Credits { get; set; }
    }
}
