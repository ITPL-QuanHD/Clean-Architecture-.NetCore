namespace ShopChain.Core.Entities
{
    public enum TaxType
    {
        Included,   // Giá đã bao gồm thuế
        Excluded,   // Giá tách thuế, cần cộng thêm
        None        // Miễn thuế
    }

    public enum DiscountType
    {
        None,       // Không giảm giá
        Amount,     // Giảm theo số tiền
        Percent     // Giảm theo %
    }

    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty!;

        public string? Url { get; set; } = string.Empty!;

        public decimal? SellPrice { get; set; } = 0;

        public decimal? OriginalPrice { get; set; } = 0;

        public string Unit { get; set; } = string.Empty!;

        public int ProductGroupID { get; set; }

        public TaxType TaxType { get; set; } = TaxType.Included;

        public decimal? TaxRate { get; set; } = 0;

        public bool IsPointAccumulateEnabled { get; set; } = true;

        public bool IsPointRedeemEnabled { get; set; } = true;

        public decimal? PointAccumulateRate { get; set; } = 0;

        public DiscountType DiscountType { get; set; } = DiscountType.None;

        public decimal? DiscountValue { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateAt { get; set; } = null;

        public ProductGroup? ProductGroup { get; set; }

        public ICollection<Inventory> inventories { get; set; } = new List<Inventory>();
        public ICollection<StockReceiptDetail> StockReceiptDetails { get; set; } = new List<StockReceiptDetail>();
        public ICollection<StockReturnDetail> StockReturnDetails { get; set; } = new List<StockReturnDetail>();
        public ICollection<StockTransferDetail> stockTransferDetails { get; set; } = new List<StockTransferDetail>();
        public ICollection<InventoryStocktakeDetail> inventoryStocktakeDetails { get; set; } = new List<InventoryStocktakeDetail>();

        /// <summary>
        /// Tính giá bán sau khi áp dụng giảm giá (nếu có).
        /// </summary>
        /// <returns>Giá bán sau khi áp dụng giảm giá</returns>
        public decimal GetDiscountedPrice()
        {
            if (DiscountType == DiscountType.Amount)
                return Math.Max(0, (SellPrice ?? 0) - (DiscountValue ?? 0));
            else if (DiscountType == DiscountType.Percent)
                return Math.Max(0, (SellPrice ?? 0) * (1 - (DiscountValue ?? 0) / 100));
            else
                return SellPrice ?? 0;
        }

        /// <summary>
        /// Tính số điểm tích luỹ cho sản phẩm này.
        /// </summary>
        /// <param name="defaultRate">
        /// Tỷ lệ tích điểm mặc định nếu sản phẩm không thiết lập riêng.
        /// Truyền vào dưới dạng phần trăm (ví dụ: 1 nghĩa là 1%).
        /// </param>
        /// <returns>Số điểm được cộng (làm tròn xuống)</returns>
        public int GetAccumulatePoints(decimal defaultRate = 1)
        {
            // Nếu không cho tích điểm, trả về 0
            if (!IsPointAccumulateEnabled)
                return 0;

            // Lấy tỷ lệ ưu tiên theo sản phẩm, nếu null thì lấy mặc định truyền vào
            decimal rate = (PointAccumulateRate ?? defaultRate) / 100;

            // Giá dùng để tính điểm là giá sau khi giảm (chưa tính thuế)
            decimal basePrice = GetDiscountedPrice();

            int points = (int)Math.Floor(basePrice * rate);

            return points;
        }

        /// <summary>
        /// Tính giá bán cuối cùng sau thuế, đã áp dụng giảm giá.
        /// </summary>
        /// <returns>Giá sau thuế (đã giảm giá)</returns>
        public decimal GetFinalPrice()
        {
            decimal price = GetDiscountedPrice();

            switch (TaxType)
            {
                case TaxType.Excluded:
                    // Giá chưa có thuế, cộng thêm thuế
                    decimal tax = (price * (TaxRate ?? 0)) / 100;
                    return price + tax;

                case TaxType.Included:
                case TaxType.None:
                default:
                    // Giá đã bao gồm thuế hoặc miễn thuế, trả lại giá gốc
                    return price;
            }
        }

        /// <summary>
        /// Hàm tính lợi nhuận từ sản phẩm này.
        /// </summary>
        /// <returns></returns>
        public decimal GetProfit()
        {
            return GetFinalPrice() - (OriginalPrice ?? 0);
        }

        /// <summary>
        /// Tính giá trị tiền thuế phải nộp cho sản phẩm (đã áp dụng giảm giá).
        /// </summary>
        /// <returns>Tiền thuế thực tế, luôn >= 0</returns>
        public decimal GetActualTaxAmount()
        {
            decimal price = GetDiscountedPrice();

            switch (TaxType)
            {
                case TaxType.Excluded:
                    // Giá chưa bao gồm thuế, tính trên giá đã giảm
                    return (price * (TaxRate ?? 0)) / 100;

                case TaxType.Included:
                    // Giá đã bao gồm thuế, phải tách phần thuế ra khỏi giá
                    // Số tiền thuế = Giá đã giảm * Tỷ lệ thuế / (100 + Tỷ lệ thuế)
                    return ((price * (TaxRate ?? 0)) / (100 + (TaxRate ?? 0)));

                case TaxType.None:
                default:
                    // Miễn thuế
                    return 0;
            }
        }

    }

}
