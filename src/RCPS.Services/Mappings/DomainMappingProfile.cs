using AutoMapper;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;

namespace RCPS.Services.Mappings;

public class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty))
            .ForMember(dest => dest.Milestones, opt => opt.MapFrom(src => src.Milestones))
            .ForMember(dest => dest.StatementsOfWork, opt => opt.MapFrom(src => src.StatementsOfWork))
            .ForMember(dest => dest.ChangeRequests, opt => opt.MapFrom(src => src.ChangeRequests))
            .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => src.Invoices));

        CreateMap<ProjectMilestone, ProjectMilestoneDto>();
        CreateMap<StatementOfWork, StatementOfWorkDto>();
        CreateMap<ChangeRequest, ChangeRequestDto>();
        CreateMap<ChangeRequest, ChangeRequestSummaryDto>();
        CreateMap<EffortEntry, EffortEntryDto>()
            .ForMember(dest => dest.TeamMemberName, opt => opt.MapFrom(src => src.TeamMember != null ? $"{src.TeamMember.FirstName} {src.TeamMember.LastName}" : string.Empty));
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines));
        CreateMap<Invoice, InvoiceSummaryDto>();
        CreateMap<InvoiceLine, InvoiceLineDto>();
        CreateMap<Reminder, ReminderDto>();
        CreateMap<RevenueRecognition, RevenueRecognitionDto>();
        CreateMap<Client, ClientDto>();
        CreateMap<TeamMember, TeamMemberDto>();
        CreateMap<ProjectAssignment, ProjectAssignmentDto>()
            .ForMember(dest => dest.TeamMemberName, opt => opt.MapFrom(src => src.TeamMember != null ? $"{src.TeamMember.FirstName} {src.TeamMember.LastName}" : string.Empty));
    }
}
