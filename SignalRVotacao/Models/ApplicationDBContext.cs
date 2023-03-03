using Microsoft.EntityFrameworkCore;

namespace SignalRVotacao.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
    }
}
