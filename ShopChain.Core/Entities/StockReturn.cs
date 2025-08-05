using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Core.Entities
{
    public class StockReturn
    {
        public int ID { get; set; }

        public string ReturnNumber { get; set; } = string.Empty;

        public DateTime ReturnDate { get; set; }

        public int StoreID { get; set; }

        public int SupplierID { get; set; }

        public string? Reason { get; set; }

        public string? Note { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public Store? Store { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<StockReturnDetail> Details { get; set; } = new List<StockReturnDetail>();
    }
}
