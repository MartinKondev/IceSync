using System.Diagnostics.CodeAnalysis;
using IceSync.Domain.Models;

namespace IceSync.Domain.Comparers
{
    public class WorkflowEqualityComparer : IEqualityComparer<WorkflowDto>
    {
        public bool Equals(WorkflowDto? x, WorkflowDto? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] WorkflowDto obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.Id.GetHashCode();
        }
    }
}