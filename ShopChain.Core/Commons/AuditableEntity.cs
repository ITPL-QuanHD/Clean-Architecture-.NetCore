using ShopChain.Core.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Core.Entities
{
    public abstract class AuditableEntity
    {
        /// <summary>ID của bản ghi</summary>

        [Key]
        [StringLength(30, MinimumLength = 30)]
        public string? Id { get; set; } = Helper.GenerateRandomString(30);

        /// <summary>Cờ xóa mềm</summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>Ngày tạo bản ghi</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>Ngày cập nhật gần nhất</summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary> Tạo Id tư động cho các thực thể kế thừa AuditableEntity </summary>
   
}
