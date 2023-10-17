using IceSync.Domain.Models.Configuration;
using IceSync.Infrastructure.ExternalApis;
using IceSync.Infrastructure.Sql;
using IceSync.Infrastructure.Workers;

namespace IceSync
{
    public static class Configuration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<UniversalLoaderConfig>(builder.Configuration.GetSection("UniversalLoader"));

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<UniversalLoaderClient>();
            builder.Services.AddSql(builder.Configuration.GetConnectionString("DefaultConnection"));
            builder.Services.AddMemoryCache();
            builder.Services.AddHostedService<WorkflowsWorker>();

        }
    }
}
