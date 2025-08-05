namespace ShopChain.Core.Entities
{
    public class StockReceipt
    {
        public int ID { get; set; }

        public string? ReceiptNumber { get; set; } = string.Empty;

        public DateTime ReceiptDate { get; set; }

        public int StoreID { get; set; }

        public int SupplierID { get; set; }

        public string? Note { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; } = false;

        public Store? Store { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<StockReceiptDetail> Details { get; set; } = new List<StockReceiptDetail>();

    }
}
