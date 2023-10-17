using IceSync.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IceSync.Infrastructure.Sql
{
    public class IceSyncDbContext : DbContext
    {
        public IceSyncDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowEntity>()
                .Property(x => x.Id).ValueGeneratedNever();

        }

        public DbSet<WorkflowEntity> Workflows { get; set; }
    }
}
