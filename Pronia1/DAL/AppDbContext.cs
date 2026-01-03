using Microsoft.EntityFrameworkCore;
namespace Pronia1.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Pronia1.Models.Slider> Sliders { get; set; }
        public DbSet<Pronia1.Models.Blog> Blogs { get; set; }
        public DbSet<Pronia1.Models.Product> Products { get; set; }
        public DbSet<Pronia1.Models.Category> Categories { get; set; }
        public DbSet<Pronia1.Models.ProductImage> ProductImages { get; set; }
    }
}
