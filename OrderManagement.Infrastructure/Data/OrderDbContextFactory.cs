using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderManagement.Infrastructure.Data
{
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            // Buraya kendi SQL Server bağlantı cümleni yaz
            optionsBuilder.UseSqlServer("Server=DESKTOP-4VJACC6;Database=OrderDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new OrderDbContext(optionsBuilder.Options);
        }
    }
}
    