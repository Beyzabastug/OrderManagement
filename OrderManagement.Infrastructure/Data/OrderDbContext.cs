using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Infrastructure.Entities; // AppUser sınıfı burada

namespace OrderManagement.Infrastructure.Data
{
    public class OrderDbContext : IdentityDbContext<AppUser>
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        // Veritabanındaki tablolar
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }    

        // Veritabanı ayarları (gerekirse override edilebilir)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // İlişkiler, kurallar buraya yazılır. Şimdilik boş bırakıyoruz.
        }
    }
}
