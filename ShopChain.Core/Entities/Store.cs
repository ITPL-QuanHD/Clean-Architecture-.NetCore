using System.ComponentModel.DataAnnotations;

namespace ShopChain.Core.Entities
{
    /// <summary>
    /// Thực thể đại diện cho Cửa hàng
    /// </summary>
    public class Store
    {
        /// <summary>
        /// Mã định danh cửa hàng (Primary Key)
        /// </summary>
        [Key]
        public int StoreID { get; set; }

        /// <summary>
        /// Mã cửa hàng (code định danh ngắn)
        /// </summary>
        [MaxLength(50)]
        public string StoreCode { get; set; } = null!;

        /// <summary>
        /// Tên cửa hàng
        /// </summary>
        [MaxLength(200)]
        public string StoreName { get; set; } = null!;

        /// <summary>
        /// Địa chỉ cửa hàng
        /// </summary>
        [MaxLength(300)]
        public string StoreAddress { get; set; } = null!;

        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        /// <summary>
        /// Email cửa hàng
        /// </summary>
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Mã số thuế
        /// </summary>
        [MaxLength(50)]
        public string TaxID { get; set; } = null!;

        /// <summary>
        /// Người đại diện pháp luật
        /// </summary>
        [MaxLength(100)]
        public string LegalRepresentative { get; set; } = null!;

        /// <summary>
        /// Tên người quản lý cửa hàng
        /// </summary>
        [MaxLength(100)]
        public string ManagerName { get; set; } = null!;

        /// <summary>
        /// Ngày khai trương
        /// </summary>
        public DateTime? OpenDate { get; set; }

        /// <summary>
        /// Ngày đóng cửa (nếu có)
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// Trạng thái hoạt động (Active / Inactive / Maintenance...)
        /// </summary>
        [MaxLength(20)]
        public string ActiveStatus { get; set; } = null!;

        /// <summary>
        /// Đường dẫn logo cửa hàng
        /// </summary>
        [MaxLength(300)]
        public string LogoUrl { get; set; } = null!;

        /// <summary>
        /// Ghi chú nội bộ
        /// </summary>
        [MaxLength(500)]
        public string Note { get; set; } = null!;

        /// <summary>
        /// Cờ xóa mềm
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        [MaxLength(50)]
        public string CreatedBy { get; set; } = null!;

        /// <summary>
        /// Ngày cập nhật gần nhất
        /// </summary>
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Người cập nhật cuối
        /// </summary>
        [MaxLength(50)]
        public string UpdatedBy { get; set; } = null!;

        public ICollection<Inventory> inventories { get; set; } = new List<Inventory>();
        public ICollection<StockReceipt> stockReceipts { get; set; } = new List<StockReceipt>();
        public ICollection<StockReturnDetail> stockReturnDetails { get; set; } = new List<StockReturnDetail>();
        public ICollection<StockReturn> stockReturns { get; set; } = new List<StockReturn>();
        public ICollection<StockTransfer> stockTransfers { get; set; } = new List<StockTransfer>();
        public ICollection<InventoryStocktake> inventoryStocktakes { get; set; } = new List<InventoryStocktake>();
    }
}
