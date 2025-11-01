using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IProjectAssignmentRepository : IGenericRepository<ProjectAssignment>
{
    Task<IReadOnlyList<ProjectAssignment>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
