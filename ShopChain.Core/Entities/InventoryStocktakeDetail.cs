using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Core.Entities
{
    public class InventoryStocktakeDetail
    {
        public int ID { get; set; }
        public int StocktakeID { get; set; }
        public InventoryStocktake? Stocktake { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }

        public decimal SystemQuantity { get; set; }    // Số lượng hệ thống (Inventory.Quantity tại thời điểm kiểm kê)
        public decimal ActualQuantity { get; set; }    // Số lượng thực tế kiểm kê
        public decimal AdjustedQuantity => ActualQuantity - SystemQuantity; // Chênh lệch (tự động tính)
        public string? Reason { get; set; }            // Lý do điều chỉnh (nếu có chênh lệch)
    }
}
