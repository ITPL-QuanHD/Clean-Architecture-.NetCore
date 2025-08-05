namespace ShopChain.Core.Entities
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public ICollection<StockReceipt> StockReceipts { get; set; } = new List<StockReceipt>();
        public ICollection<StockReturn> StockReturns { get; set; } = new List<StockReturn>();
    }
}
