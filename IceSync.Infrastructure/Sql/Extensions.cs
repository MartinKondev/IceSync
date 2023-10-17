using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IceSync.Infrastructure.Sql
{
    public static class Extensions
    {
        public static void AddSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IceSyncDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
