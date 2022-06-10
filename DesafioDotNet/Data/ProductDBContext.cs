using DESAFIOKHIPO.DesafioDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace DESAFIOKHIPO.DesafioDotNet.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}