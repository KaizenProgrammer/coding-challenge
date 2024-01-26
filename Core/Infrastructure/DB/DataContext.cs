using Microsoft.EntityFrameworkCore;
using System.IO;

namespace coding_challenge.Core.Infrastructure.DB
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public string DbPath { get; }
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            /* var folder = Environment.SpecialFolder.LocalApplicationData;
             var path = Environment.GetFolderPath(folder);
             DbPath = System.IO.Path.Join(path, Configuration.GetConnectionString("DB"));*/
            DbPath = Configuration.GetConnectionString("DB");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite($"Data Source={DbPath}");
        }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}
