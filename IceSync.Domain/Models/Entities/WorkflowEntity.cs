namespace IceSync.Domain.Models.Entities
{
    public class WorkflowEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsRunning { get; set; }

        public string MultiExecBehavior { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
