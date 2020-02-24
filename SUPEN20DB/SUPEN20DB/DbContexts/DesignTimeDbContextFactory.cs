using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SUPEN20DB.DbContexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SUPEN20DbContext>
    {
        public SUPEN20DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../SUPEN20DB/config.json").Build();
            var builder = new DbContextOptionsBuilder<SUPEN20DbContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);
            return new SUPEN20DbContext(builder.Options);
        }
    }
}
