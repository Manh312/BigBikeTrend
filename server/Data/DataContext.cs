using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config) : base(options)
        {
            this._config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config["ConnectionStrings:Auth"] ?? throw new Exception("Chuỗi kết nối bị thiếu.");
            string dbName = _config["ConnectionStrings:DbName"] ?? throw new Exception("Tên cơ sở dữ liệu bị thiếu.");
            string dbUser = _config["ConnectionStrings:DbUserId"] ?? throw new Exception("Tên người dùng cơ sở dữ liệu bị thiếu.");
            string dbPassword = _config["ConnectionStrings:DbUserPassword"] ?? throw new Exception("Mật khẩu người dùng cơ sở dữ liệu bị thiếu.");

            string ConnectionStrings = String.Format(connectionString, dbName, dbUser, dbPassword);

            optionsBuilder.UseSqlServer(ConnectionStrings);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.OriginalPrice)
                .HasPrecision(18, 2);

                entity.Property(p => p.DiscountPercentage)
                .HasColumnType("decimal(5,2)")
                .IsRequired(false);

                entity.Property(p => p.DiscountAmount)
                .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Thumbnail)
                .WithOne(i => i.Product)
                .HasForeignKey<Product>(p => p.ThumbnailId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProductDetails>(entity =>
            {
                entity.HasKey(pd => pd.Id);
                entity.Property(pd => pd.Details)
                    .HasColumnType("nvarchar(max)"); // Định nghĩa kiểu JSON (SQL Server)

                entity.HasOne(pd => pd.Product)
                    .WithMany() // Giả sử một sản phẩm có thể có nhiều chi tiết (nếu cần mối quan hệ ngược, điều chỉnh)
                    .HasForeignKey(pd => pd.ProductId)
                    .OnDelete(DeleteBehavior.Cascade); // Xóa cascade khi Product bị xóa
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(p => p.Image)
                    .WithOne(i => i.ProductCategories)
                    .HasForeignKey<ProductCategories>(p => p.ImageId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(b => b.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(p => p.Image)
                    .WithOne(i => i.Brand)
                    .HasForeignKey<Brand>(p => p.ImageId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}