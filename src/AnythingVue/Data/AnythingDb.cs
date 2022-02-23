using Anything.Models;
using Microsoft.EntityFrameworkCore;

namespace Anything.Data
{
    public class AnythingDb : DbContext
    {
        public AnythingDb(DbContextOptions<AnythingDb> options)
            : base(options)
        {
            Things = base.Set<AnyThing>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<AnyThing> Things { get; set; }
    }
}