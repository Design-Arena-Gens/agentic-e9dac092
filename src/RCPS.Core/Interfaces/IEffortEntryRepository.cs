using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IEffortEntryRepository : IGenericRepository<EffortEntry>
{
    Task<IReadOnlyList<EffortEntry>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
