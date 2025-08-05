using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopChain.Core.Entities
{
    /// <summary>
    /// Thực thể Quận/Huyện
    /// </summary>
    public class District
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Mã quận/huyện (Primary Key)
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Tên quận/huyện
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Mã tên dạng chuỗi
        /// </summary>
        public string? CodeName { get; set; }

        /// <summary>
        /// Loại đơn vị hành chính (quận/huyện/thị xã)
        /// </summary>
        public string? DivisionType { get; set; }

        /// <summary>
        /// Tên viết tắt
        /// </summary>
        public string? ShortCodeName { get; set; }

        /// <summary>
        /// Mã tỉnh/thành (foreign key)
        /// </summary>
        public int ProvinceCode { get; set; }

        /// <summary>
        /// Thông tin tỉnh/thành cha (navigation property)
        /// </summary>
        public Province? Province { get; set; }

        /// <summary>
        /// Danh sách phường/xã thuộc quận/huyện
        /// </summary>
        public List<Ward> Wards { get; set; } = new();
    }
}
