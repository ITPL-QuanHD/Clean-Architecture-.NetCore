namespace ShopChain.Core.Entities
{
    public class StockReceiptDetail
    {
        public int ID { get; set; }

        public int ReceiptID { get; set; }

        public int ProductID { get; set; }  

        public decimal Quantity { get; set; }

        public decimal UnitPrice{ get; set; }

        public string? Note { get; set; } = string.Empty;

        public StockReceipt? Receipt { get; set; }
        public Product? Product { get; set; }
    }
}
