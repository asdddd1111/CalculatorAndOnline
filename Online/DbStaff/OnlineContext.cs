using Microsoft.EntityFrameworkCore;
using Online.DbStaff.Model;

namespace Online.DbStaff
{
    public class OnlineContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public OnlineContext(DbContextOptions dbContext) : base(dbContext) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
