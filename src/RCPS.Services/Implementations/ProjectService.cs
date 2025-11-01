using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProjectRequest> _createValidator;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(
        IProjectRepository projectRepository,
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateProjectRequest> createValidator,
        ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository;
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _logger = logger;
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        await _createValidator.ValidateAndThrowAsync(request, cancellationToken);

        if (!await _clientRepository.ExistsAsync(request.ClientId, cancellationToken))
        {
            throw new InvalidOperationException($"Client '{request.ClientId}' does not exist.");
        }

        var project = new Project
        {
            Name = request.Name,
            ClientId = request.ClientId,
            Description = request.Description,
            Status = request.Status,
            Health = request.Health,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            BudgetAmount = request.BudgetAmount,
            BudgetHours = request.BudgetHours
        };

        await _projectRepository.AddAsync(project, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created project {ProjectId} for client {ClientId}", project.Id, project.ClientId);

        var created = await _projectRepository.GetFullProjectAsync(project.Id, cancellationToken)
            ?? throw new InvalidOperationException("Failed to load created project.");

        return _mapper.Map<ProjectDto>(created);
    }

    public async Task<ProjectDto?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFullProjectAsync(projectId, cancellationToken);
        return project is null ? null : _mapper.Map<ProjectDto>(project);
    }

    public async Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.GetActiveProjectsAsync(cancellationToken);
        return projects.Select(p => _mapper.Map<ProjectDto>(p)).ToList();
    }

    public async Task<ProjectDto?> UpdateProjectAsync(Guid projectId, UpdateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetAsync(projectId, cancellationToken);
        if (project is null)
        {
            return null;
        }

        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status;
        project.Health = request.Health;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.BudgetAmount = request.BudgetAmount;
        project.BudgetHours = request.BudgetHours;

        _projectRepository.Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updated = await _projectRepository.GetFullProjectAsync(projectId, cancellationToken);
        return updated is null ? null : _mapper.Map<ProjectDto>(updated);
    }

    public async Task<bool> ArchiveProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetAsync(projectId, cancellationToken);
        if (project is null)
        {
            return false;
        }

        _projectRepository.Remove(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
