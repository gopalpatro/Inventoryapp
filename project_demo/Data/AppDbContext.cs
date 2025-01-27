using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using project_demo.Model;

namespace project_demo.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Registrationrequest> Registrationrequests { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Product> products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Stock> stocks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //public DbSet<User> Users { get; set; }
        
    }
}
 