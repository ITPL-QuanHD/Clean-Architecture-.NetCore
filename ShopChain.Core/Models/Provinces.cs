namespace ShopChain.Core.Models
{
    /// <summary>
    /// Mô hình dữ liệu tỉnh/thành từ nguồn API bên ngoài
    /// </summary>
    public class Province
    {
        public string Name { get; set; } = string.Empty;

        public int Code { get; set; }

        public string CodeName { get; set; } = string.Empty;

        public string DivisionType { get; set; } = string.Empty;

        public int PhoneCode { get; set; }

        public List<District> Districts { get; set; } = new();
    }

    /// <summary>
    /// Mô hình dữ liệu quận/huyện từ API
    /// </summary>
    public class District
    {
        public string Name { get; set; } = string.Empty;

        public int Code { get; set; }

        public string CodeName { get; set; } = string.Empty;

        public string DivisionType { get; set; } = string.Empty;

        public string ShortCodeName { get; set; } = string.Empty;

        public List<Ward> Wards { get; set; } = new();
    }

    /// <summary>
    /// Mô hình dữ liệu phường/xã từ API
    /// </summary>
    public class Ward
    {
        public string Name { get; set; } = string.Empty;

        public int Code { get; set; }

        public string CodeName { get; set; } = string.Empty;

        public string DivisionType { get; set; } = string.Empty;

        public string ShortCodeName { get; set; } = string.Empty;
    }
}
