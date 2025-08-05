namespace ShopChain.Core.Entities
{
    public class StockTransfer
    {
        public int ID { get; set; }
        public string TransferNumber { get; set; } = string.Empty;
        public DateTime TransferDate { get; set; }
        public int StoreID_From { get; set; }
        public int StoreID_To { get; set; }
        public string? Reason { get; set; }
        public string? Note { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public DateTime? ConfirmedAt { get; set; }
        public string? ConfirmedBy { get; set; }

        public Store? StoreFrom { get; set; }
        public Store? StoreTo { get; set; }
        public ICollection<StockTransferDetail> Details { get; set; } = new List<StockTransferDetail>();
    }
}
