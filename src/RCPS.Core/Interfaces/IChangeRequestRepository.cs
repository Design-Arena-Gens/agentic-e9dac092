using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IChangeRequestRepository : IGenericRepository<ChangeRequest>
{
    Task<IReadOnlyList<ChangeRequest>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
