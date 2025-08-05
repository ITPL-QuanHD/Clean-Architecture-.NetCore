using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Core.Entities
{
    public class InventoryStocktake
    {
        public int ID { get; set; }
        public string StocktakeNumber { get; set; } = string.Empty; 
        public DateTime StocktakeDate { get; set; }                
        public int StoreID { get; set; }                            
        public string? PerformedBy { get; set; }                    
        public string? Note { get; set; }                           
        public bool IsConfirmed { get; set; } = false;              
        public DateTime? ConfirmedAt { get; set; }                  
        public string? ConfirmedBy { get; set; }                    
        public Store? Store { get; set; }
        public ICollection<InventoryStocktakeDetail> Details { get; set; } = new List<InventoryStocktakeDetail>();
    }
}
