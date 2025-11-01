using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IRevenueRecognitionService
{
    Task<RevenueRecognitionDto> CreateAsync(CreateRevenueRecognitionRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RevenueRecognitionDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
