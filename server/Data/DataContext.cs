using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config): base(options)
        {
            this._config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config["ConnectionStrings:Auth"]?? throw new Exception("Connection string missing");
            string dbName = _config["ConnectionStrings:DbName"] ?? throw new Exception("DbName Missing");
            string dbUser = _config["ConnectionStrings:DbUserId"] ?? throw new Exception("Db UserName Missing");
            string dbPassword = _config["ConnectionStrings:DbUserPassword"] ?? throw new Exception("Db User Password Missing");

            string ConnectionStrings = String.Format(connectionString, dbName, dbUser, dbPassword);

            optionsBuilder.UseSqlServer(ConnectionStrings);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Thumbnail)
                .WithOne(i => i.Product)
                .HasForeignKey<Product>(p => p.ThumbnailId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProductCategories>()
                .HasOne(p => p.Image)
                .WithOne(i => i.ProductCategories)
                .HasForeignKey<ProductCategories>(p => p.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Brand>()
                .HasOne(p => p.Image)
                .WithOne(i => i.Brand)
                .HasForeignKey<Brand>(p => p.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
