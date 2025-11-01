using AutoMapper;
using FluentValidation;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateReminderRequest> _validator;

    public ReminderService(
        IReminderRepository reminderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateReminderRequest> validator)
    {
        _reminderRepository = reminderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ReminderDto> ScheduleReminderAsync(CreateReminderRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var reminder = new Reminder
        {
            Type = request.Type,
            ProjectId = request.ProjectId,
            InvoiceId = request.InvoiceId,
            ScheduledFor = request.ScheduledFor,
            Message = request.Message
        };

        await _reminderRepository.AddAsync(reminder, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ReminderDto>(reminder);
    }

    public async Task<IReadOnlyList<ReminderDto>> GetPendingRemindersAsync(DateTime asOf, CancellationToken cancellationToken = default)
    {
        var reminders = await _reminderRepository.GetPendingRemindersAsync(asOf, cancellationToken);
        return reminders.Select(r => _mapper.Map<ReminderDto>(r)).ToList();
    }

    public async Task MarkAsSentAsync(Guid reminderId, CancellationToken cancellationToken = default)
    {
        var reminder = await _reminderRepository.GetAsync(reminderId, cancellationToken);
        if (reminder is null)
        {
            return;
        }

        reminder.IsSent = true;
        _reminderRepository.Update(reminder);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
