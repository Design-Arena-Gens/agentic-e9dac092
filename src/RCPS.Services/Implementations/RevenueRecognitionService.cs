using AutoMapper;
using FluentValidation;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class RevenueRecognitionService : IRevenueRecognitionService
{
    private readonly IRevenueRecognitionRepository _repository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateRevenueRecognitionRequest> _validator;

    public RevenueRecognitionService(
        IRevenueRecognitionRepository repository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateRevenueRecognitionRequest> validator)
    {
        _repository = repository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<RevenueRecognitionDto> CreateAsync(CreateRevenueRecognitionRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var project = await _projectRepository.GetAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new InvalidOperationException($"Project {request.ProjectId} not found.");
        }

        var recognition = new RevenueRecognition
        {
            ProjectId = request.ProjectId,
            RecognitionDate = request.RecognitionDate,
            Amount = request.Amount,
            Status = request.Status,
            Notes = request.Notes
        };

        await _repository.AddAsync(recognition, cancellationToken);
        project.RevenueRecognized += recognition.Amount;
        _projectRepository.Update(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RevenueRecognitionDto>(recognition);
    }

    public async Task<IReadOnlyList<RevenueRecognitionDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var recognitions = await _repository.GetByProjectAsync(projectId, cancellationToken);
        return recognitions.Select(r => _mapper.Map<RevenueRecognitionDto>(r)).ToList();
    }
}
