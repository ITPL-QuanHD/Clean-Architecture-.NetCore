namespace ShopChain.Core.Entities
{
    public class ProductGroup
    {
        public int ID { get; set; }

        public string? GroupCode { get; set; } = string.Empty;

        public string GroupName { get; set; } = string.Empty;

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
