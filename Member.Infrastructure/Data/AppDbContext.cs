using Microsoft.EntityFrameworkCore;
using Member.Domain.Entities;

namespace Member.Infrastructure.Data
{
     public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Members> Members { get; set; }
    }
}
