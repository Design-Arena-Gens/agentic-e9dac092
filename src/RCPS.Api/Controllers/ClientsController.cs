using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients(CancellationToken cancellationToken)
    {
        var clients = await _clientService.GetClientsAsync(cancellationToken);
        return Ok(clients);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = await _clientService.CreateClientAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetClients), new { clientId = client.Id }, client);
    }
}
