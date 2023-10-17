using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IceSync.Infrastructure.Sql
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<IceSyncDbContext>
    {
        public IceSyncDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IceSyncDbContext>();
            optionsBuilder.UseSqlServer("Data Source=BGMOB40048\\SQLEXPRESS;Initial Catalog=IceSync;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new IceSyncDbContext(optionsBuilder.Options);
        }
    }
}
