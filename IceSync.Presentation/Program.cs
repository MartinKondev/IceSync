using IceSync;
using IceSync.Application.Middleware;
using IceSync.Infrastructure.Sql;
using IceSync.Infrastructure.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);

builder.ConfigureServices();

builder.Services.AddSql(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<WorkflowsWorker>();
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<WorkflowsWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Workflows}/{action=Index}/{id?}");

app.UseMiddleware<BearerMiddleware>();

app.Run();
