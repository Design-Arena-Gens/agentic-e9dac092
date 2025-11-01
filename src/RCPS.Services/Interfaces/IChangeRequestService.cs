using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IChangeRequestService
{
    Task<ChangeRequestDto> CreateAsync(CreateChangeRequestRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChangeRequestDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<ChangeRequestDto?> UpdateStatusAsync(Guid changeRequestId, ChangeRequestDto updated, CancellationToken cancellationToken = default);
}
