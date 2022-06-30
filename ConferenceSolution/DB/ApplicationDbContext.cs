using ConferenceSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSolution.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<Participant> Participants { get; set; }
    }
}
