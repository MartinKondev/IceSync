using IceSync.Domain;
using IceSync.Infrastructure.ExternalApis;
using IceSync.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IceSync.Infrastructure.Workers
{
    public class WorkflowsWorker : BackgroundService
    {
        private readonly IceSyncDbContext _dbContext;
        private readonly UniversalLoaderClient _universalLoaderClient;
        private readonly IConfiguration _configuration;

        public WorkflowsWorker(IceSyncDbContext dbContext, UniversalLoaderClient universalLoaderClient, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _universalLoaderClient = universalLoaderClient;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = int.Parse(_configuration["BackgroundServiceInterval"]);

            await Console.Out.WriteLineAsync($"WorkflowsWorker ExecuteAsync started. Interval: {interval} mins");

            var timer = new PeriodicTimer(TimeSpan.FromMinutes(interval));
            do
            {
                var bearer = await _universalLoaderClient.Authenticate();
                var fetchedWorkflows = await _universalLoaderClient.GetWorkflowsList(bearer.Token);
                var existing = await _dbContext.Workflows.ToListAsync();

                // Add
                var newWorkflowsDtos = fetchedWorkflows
                    .Where(f => !existing.Select(x => x.Id)
                    .Any(e => f.Id == e))
                    .ToList();
                var mapped = newWorkflowsDtos.Select(x => x.MapFromDto());
                _dbContext.AddRange(mapped);

                // Remove
                var forDeletion = existing
                    .Where(f => !fetchedWorkflows.Select(x => x.Id)
                    .Any(e => f.Id == e));
                _dbContext.RemoveRange(forDeletion);

                // Update
                await _dbContext.Workflows.ForEachAsync(x =>
                {
                    x = x.UpdateFromDto(fetchedWorkflows.First(e => e.Id == x.Id));
                });

                _dbContext.SaveChanges();

                await Console.Out.WriteLineAsync($"Worker Run at {DateTime.Now}");
            }
            while (await timer.WaitForNextTickAsync());
        }
    }
}
