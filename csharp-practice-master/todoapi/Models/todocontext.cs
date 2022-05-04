using Microsoft.EntityFrameworkCore;

namespace todoapi.Models
{
    public class todocontext : DbContext
    {
        public todocontext(DbContextOptions<todocontext> options) : base(options)
        {

        }
        public DbSet<todoitem> todoitems { get; set; }
    }
}
