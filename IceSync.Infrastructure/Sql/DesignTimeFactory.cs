using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IceSync.Infrastructure.Sql
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<IceSyncDbContext>
    {
        public IceSyncDbContext CreateDbContext(string[] args)
        {
            var connectoinString = "";
            var optionsBuilder = new DbContextOptionsBuilder<IceSyncDbContext>();
            optionsBuilder.UseSqlServer(connectoinString);

            return new IceSyncDbContext(optionsBuilder.Options);
        }
    }
}
