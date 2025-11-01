using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<Project?> GetFullProjectAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Project>> GetActiveProjectsAsync(CancellationToken cancellationToken = default);
}
