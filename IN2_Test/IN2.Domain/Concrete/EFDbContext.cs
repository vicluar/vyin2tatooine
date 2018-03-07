using System.Data.Entity;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CitizenStatus> CitizenStatus { get; set; }
    }
}
