namespace ShopChain.Core.Entities
{
    public class StockTransferDetail
    {
        public int ID { get; set; }
        public int TransferID { get; set; }
        public StockTransfer? Transfer { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public decimal Quantity { get; set; }
        public string? Note { get; set; }
    }
}
