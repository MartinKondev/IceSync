using IceSync.Domain.Models.Configuration;
using IceSync.Infrastructure.ExternalApis;

namespace IceSync
{
    public static class Configuration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<UniversalLoaderConfig>(builder.Configuration.GetSection("UniversalLoader"));

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddScoped<UniversalLoaderClient>();
        }
    }
}
