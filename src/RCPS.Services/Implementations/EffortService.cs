using AutoMapper;
using FluentValidation;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class EffortService : IEffortService
{
    private readonly IEffortEntryRepository _effortEntryRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateEffortEntryRequest> _validator;

    public EffortService(
        IEffortEntryRepository effortEntryRepository,
        IProjectRepository projectRepository,
        ITeamMemberRepository teamMemberRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateEffortEntryRequest> validator)
    {
        _effortEntryRepository = effortEntryRepository;
        _projectRepository = projectRepository;
        _teamMemberRepository = teamMemberRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<EffortEntryDto> LogEffortAsync(CreateEffortEntryRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var project = await _projectRepository.GetAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new InvalidOperationException($"Project {request.ProjectId} not found.");
        }

        var teamMember = await _teamMemberRepository.GetAsync(request.TeamMemberId, cancellationToken);
        if (teamMember is null)
        {
            throw new InvalidOperationException($"Team member {request.TeamMemberId} not found.");
        }

        var effort = new EffortEntry
        {
            ProjectId = request.ProjectId,
            TeamMemberId = request.TeamMemberId,
            EffortType = request.EffortType,
            WorkDate = request.WorkDate,
            Hours = request.Hours,
            BillRate = request.BillRate,
            CostRate = request.CostRate,
            Notes = request.Notes
        };

        await _effortEntryRepository.AddAsync(effort, cancellationToken);
        project.ActualHours += request.Hours;
        project.ActualAmount += request.Hours * request.BillRate;
        project.CostIncurred += request.Hours * request.CostRate;
        _projectRepository.Update(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EffortEntryDto>(effort);
    }

    public async Task<IReadOnlyList<EffortEntryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var efforts = await _effortEntryRepository.GetByProjectAsync(projectId, cancellationToken);
        return efforts.Select(e => _mapper.Map<EffortEntryDto>(e)).ToList();
    }
}
