using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Core.Entities
{
    public class StockReturnDetail
    {
        public int ID { get; set; }

        public int ReturnID { get; set; }

        public int ProductID { get; set; }

        public decimal Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? Note { get; set; } = string.Empty;

        public Store? Store { get; set; }
        public StockReturn? Return { get; set; }
        public Product? Product { get; set; }
    }
}
