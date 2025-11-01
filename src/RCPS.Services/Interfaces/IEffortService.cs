using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IEffortService
{
    Task<EffortEntryDto> LogEffortAsync(CreateEffortEntryRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EffortEntryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
