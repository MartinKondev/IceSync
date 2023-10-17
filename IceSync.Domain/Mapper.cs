using IceSync.Domain.Models;
using IceSync.Domain.Models.Entities;

namespace IceSync.Domain
{
    public static class Mapper
    {
        public static WorkflowEntity MapFromDto(this WorkflowDto src)
        {
            var dest = new WorkflowEntity
            {
                Id = src.Id,
                IsActive = src.IsActive,
                IsRunning = src.IsRunning,
                MultiExecBehavior = src.MultiExecBehavior,
                Name = src.Name,
                LastUpdated = DateTime.Now
            };

            return dest;
        }

        public static WorkflowEntity UpdateFromDto(this WorkflowEntity dest, WorkflowDto src)
        {
            dest.IsActive = src.IsActive;
            dest.IsRunning = src.IsRunning;
            dest.MultiExecBehavior = src.MultiExecBehavior;
            dest.Name = src.Name;
            dest.LastUpdated = DateTime.Now;

            return dest;
        }
    }
}
