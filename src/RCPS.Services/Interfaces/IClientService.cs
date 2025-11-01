using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IClientService
{
    Task<ClientDto> CreateClientAsync(CreateClientRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientDto>> GetClientsAsync(CancellationToken cancellationToken = default);
}
