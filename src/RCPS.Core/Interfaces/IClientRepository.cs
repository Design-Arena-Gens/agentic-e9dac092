using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
