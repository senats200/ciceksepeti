using ciceksepeti.Models;
using ciceksepeti.Models.ciceksepeti.Models;
using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserAccount> Members { get; set; }
        public DbSet<OrderInformation> OrderInformation { get; set; }
        public DbSet<CardInformation> CardInformation { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeeklySales> WeeklySales { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public  DbSet<Product> Yields { get; set; }
        public DbSet<UpdatedProduct> UpdatedProducts { get; set; }
        public DbSet<ProductSalesSummary> ProductSalesSummary { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // İlişkileri tanımlıyoruz
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

          

        

            modelBuilder.Entity<OrderInformation>()
                .HasKey(o => o.OrderInformationId);

            modelBuilder.Entity<CardInformation>()
                .ToTable("CardInformation");

            

            modelBuilder.Entity<WeeklySales>()
                .HasNoKey();
            modelBuilder.Entity<UserInfo>()
                .HasKey(od => new { od.Id });


            modelBuilder.Entity<Product>()
        .ToTable("Yields", tb => tb.HasTrigger("trg_UpdateProductPrice"));
            modelBuilder.Entity<UserInfo>()
      .ToTable("UserInfo", tb => tb.HasTrigger("trg_UserInfoUpdated"));
         
            modelBuilder.Entity<UserAccount>()
    .ToTable("Members", tb => tb.HasTrigger("trg_UpdatePassword"));
         

            modelBuilder.Entity<ProductSalesSummary>().HasNoKey();
            modelBuilder.Entity<OrderDetail>()
              .HasNoKey();
            modelBuilder.Entity<CreditCardInfo>()
              .HasNoKey();
            modelBuilder.Entity<AllSales>()
             .HasNoKey();









        }
    }
}
