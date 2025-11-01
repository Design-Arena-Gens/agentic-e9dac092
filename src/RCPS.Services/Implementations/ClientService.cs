using AutoMapper;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClientService(
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClientDto> CreateClientAsync(CreateClientRequest request, CancellationToken cancellationToken = default)
    {
        var client = new Client
        {
            Name = request.Name,
            Industry = request.Industry,
            PrimaryContactName = request.PrimaryContactName,
            PrimaryContactEmail = request.PrimaryContactEmail,
            Currency = request.Currency,
            AnnualContractValue = request.AnnualContractValue
        };

        await _clientRepository.AddAsync(client, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ClientDto>(client);
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync(CancellationToken cancellationToken = default)
    {
        var clients = await _clientRepository.ListAsync(cancellationToken);
        return clients.Select(c => _mapper.Map<ClientDto>(c)).ToList();
    }
}
