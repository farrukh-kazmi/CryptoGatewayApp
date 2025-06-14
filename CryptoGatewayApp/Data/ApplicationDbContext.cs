
using Microsoft.EntityFrameworkCore;
using CryptoGatewayApp.Models;

namespace CryptoGatewayApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Payment> Payments { get; set; }
    }
}