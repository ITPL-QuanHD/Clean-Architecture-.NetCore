using Microsoft.EntityFrameworkCore;
using ShopChain.Core.Entities;

namespace ShopChain.Infrastructure.Data
{
    /// <summary>
    /// DbContext chính dùng để truy cập và cấu hình database.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Bảng cửa hàng
        /// </summary>
        public DbSet<Store> Stores { get; set; } = null!;

        /// <summary>
        /// Bảng tỉnh thành
        /// </summary>
        public DbSet<Province> Provinces { get; set; } = null!;

        /// <summary>
        /// Bảng quận/huyện
        /// </summary>
        public DbSet<District> Districts { get; set; } = null!;

        /// <summary>
        /// Bảng phường/xã
        /// </summary>
        public DbSet<Ward> Wards { get; set; } = null!;

        /// <summary>
        /// Bảng nhân viên
        /// </summary>
        public DbSet<Employee> Employees { get; set; } = null!;

        /// <summary>
        /// Bảng phòng ban
        /// </summary>
        public DbSet<Department> Departments { get; set; } = null!;

        /// <summary>
        /// Bảng khách hàng dùng để đăng nhập ứng dụng
        /// </summary>
        public DbSet<UserClient> UserClients { get; set; }

        /// <summary>
        /// Bảng nhóm sản phẩm
        /// </summary>
        public DbSet<ProductGroup> ProductGroups { get; set; } = null!;

        /// <summary>
        /// Bảng sản phẩm
        /// </summary>
        public DbSet<Product> Products { get; set; } = null!;

        /// <summary>
        /// Bảng tồn kho theo cửa hàng
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; } = null!;

        /// <summary>
        /// Bảng nhà cung cấp
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; } = null!;

        /// <summary>
        /// Bảng phiếu nhập kho
        /// </summary>
        public DbSet<StockReceipt> StockReceipts { get; set; } = null!;

        /// <summary>
        /// Bảng chi tiết phiếu nhập kho
        /// </summary>
        public DbSet<StockReceiptDetail> StockReceiptDetails { get; set; } = null!;

        /// <summary>
        /// Bảng phiếu chuyển kho
        /// </summary>
        public DbSet<StockTransfer> StockTransfers { get; set; } = null!;

        /// <summary>
        /// Bảng chi tiết phiếu chuyển kho
        /// </summary>
        public DbSet<StockTransferDetail> StockTransferDetails { get; set; } = null!;

        /// <summary>
        /// Bảng kiểm kê
        /// </summary>
        public DbSet<InventoryStocktake> InventoryStocktakes { get; set; } = null!;

        /// <summary>
        /// Bảng chi tiết kiểm kê
        /// </summary>
        public DbSet<InventoryStocktakeDetail> InventoryStocktakeDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Quan hệ nhân viên - phòng ban ===
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict); // Không cho phép xóa phòng ban nếu còn nhân viên

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerID)
                .OnDelete(DeleteBehavior.SetNull); // Nếu manager bị xóa thì set null
                                                   // 
            modelBuilder.Entity<StockTransfer>()
                .HasOne(st => st.StoreFrom)
                .WithMany() // Không cần navigation ngược
                .HasForeignKey(st => st.StoreID_From)
                .OnDelete(DeleteBehavior.Restrict); // hoặc .NoAction nếu muốn

            modelBuilder.Entity<StockTransfer>()
                .HasOne(st => st.StoreTo)
                .WithMany()
                .HasForeignKey(st => st.StoreID_To)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
