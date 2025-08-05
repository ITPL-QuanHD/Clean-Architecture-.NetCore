using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Core.Entities
{
    public class Ward
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? CodeName { get; set; }
        public string? DivisionType { get; set; }
        public string? ShortCodeName { get; set; }
        public int DistrictCode { get; set; }
        public District? District { get; set; }
    }
}
