using Microsoft.EntityFrameworkCore;

namespace FlexeraAPI.Models
{
    public class FlexeraContext : DbContext
    {
        public FlexeraContext(DbContextOptions<FlexeraContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
    }
}
