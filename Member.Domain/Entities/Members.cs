using System.ComponentModel.DataAnnotations;

namespace Member.Domain.Entities
{
    public class Members
    {
        [Key]
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Type { get; set; }
        public string ?Address { get; set; }

    }
}
