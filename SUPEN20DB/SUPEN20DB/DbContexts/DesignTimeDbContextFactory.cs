using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SUPEN20DB.DbContexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SUPEN20DbContext> //IDesignTimeDbContextFactory interface so that we can separate the EF code needed for generating database tables at design-time (what is commonly referred to as a code-first approach) from EF code used by our application at runtime.
    {
        public SUPEN20DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../../SystemLayer/SUPEN20_SystemLayer/SystemAPI/appsettings.json").Build(); //IConfiguration in which you set the base path to the main project directory.

            var builder = new DbContextOptionsBuilder<SUPEN20DbContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("SUPEN20DB"));
            return new SUPEN20DbContext(builder.Options);
        }
    }
}