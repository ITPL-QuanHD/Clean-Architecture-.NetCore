namespace ShopChain.Core.Entities
{
    public class Inventory
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public int StoreID { get; set; }

        public decimal Quantity { get; set; } = 0;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }
        public Store? Store { get; set; }
    }
}
