using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => DbSet.AnyAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);
}
