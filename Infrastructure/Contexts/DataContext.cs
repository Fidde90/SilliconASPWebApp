using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class DataContext : IdentityDbContext<AppUserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }
        public DbSet<AddressEntity> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserEntity>()
                .HasOne(u => u.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
