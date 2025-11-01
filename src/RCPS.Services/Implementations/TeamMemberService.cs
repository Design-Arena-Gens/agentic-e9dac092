using AutoMapper;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TeamMemberService(
        ITeamMemberRepository teamMemberRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _teamMemberRepository = teamMemberRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamMemberDto> CreateAsync(CreateTeamMemberRequest request, CancellationToken cancellationToken = default)
    {
        var member = new TeamMember
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
            DefaultBillRate = request.DefaultBillRate,
            DefaultCostRate = request.DefaultCostRate
        };

        await _teamMemberRepository.AddAsync(member, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeamMemberDto>(member);
    }

    public async Task<IReadOnlyList<TeamMemberDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var members = await _teamMemberRepository.GetActiveAsync(cancellationToken);
        return members.Select(m => _mapper.Map<TeamMemberDto>(m)).ToList();
    }
}
