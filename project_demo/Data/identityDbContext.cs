using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace project_demo.Data
{
    public class identityDbContext : IdentityDbContext
    {
        public identityDbContext(DbContextOptions<identityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)// used to define relations between entities and define constraints of entities
        {
            base.OnModelCreating(builder);

            var AdminId = "960e7eeb-1339-4563-b135-c9858d2054b5";
            var UserId = "348fd3c5-3fae-4ec7-9dfa-e704c3b2e24d";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=AdminId,
                    ConcurrencyStamp=AdminId,
                    Name="Admin",
                    NormalizedName="Admin".ToUpper()
                },
                 new IdentityRole
                {
                    Id=UserId,
                    ConcurrencyStamp=UserId,
                    Name="User",
                    NormalizedName="User".ToUpper()
                }
            };
            //seed  this data into database upon migration
            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
