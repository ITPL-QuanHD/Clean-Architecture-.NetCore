using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopChain.Core.Entities
{
    /// <summary>
    /// Phòng ban trong công ty
    /// </summary>
    public class Department : AuditableEntity
    {
        /// <summary>ID phòng ban</summary>
        [Key]
        public int DepartmentID { get; set; }

        /// <summary>Tên phòng ban</summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>ID quản lý phòng ban</summary>
        public int? ManagerID { get; set; }

        /// <summary>Nhân viên quản lý</summary>
        public Employee? Manager { get; set; }

        /// <summary>Danh sách nhân viên thuộc phòng ban</summary>
        public List<Employee> Employees { get; set; } = new();
    }
}
