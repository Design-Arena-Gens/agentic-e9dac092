using AutoMapper;
using FluentValidation;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class ChangeRequestService : IChangeRequestService
{
    private readonly IChangeRequestRepository _changeRequestRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateChangeRequestRequest> _validator;

    public ChangeRequestService(
        IChangeRequestRepository changeRequestRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateChangeRequestRequest> validator)
    {
        _changeRequestRepository = changeRequestRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ChangeRequestDto> CreateAsync(CreateChangeRequestRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var project = await _projectRepository.GetAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new InvalidOperationException($"Project {request.ProjectId} not found.");
        }

        var changeRequest = new ChangeRequest
        {
            ProjectId = request.ProjectId,
            Title = request.Title,
            Description = request.Description,
            EstimatedHours = request.EstimatedHours,
            EstimatedAmount = request.EstimatedAmount
        };

        await _changeRequestRepository.AddAsync(changeRequest, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ChangeRequestDto>(changeRequest);
    }

    public async Task<IReadOnlyList<ChangeRequestDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var changeRequests = await _changeRequestRepository.GetByProjectAsync(projectId, cancellationToken);
        return changeRequests.Select(cr => _mapper.Map<ChangeRequestDto>(cr)).ToList();
    }

    public async Task<ChangeRequestDto?> UpdateStatusAsync(Guid changeRequestId, ChangeRequestDto updated, CancellationToken cancellationToken = default)
    {
        var entity = await _changeRequestRepository.GetAsync(changeRequestId, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Status = updated.Status;
        entity.DecisionDate = updated.DecisionDate;
        entity.DecisionNotes = updated.DecisionNotes;

        _changeRequestRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ChangeRequestDto>(entity);
    }
}
