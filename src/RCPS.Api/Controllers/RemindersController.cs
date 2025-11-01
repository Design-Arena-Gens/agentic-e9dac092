using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/reminders")]
public class RemindersController : ControllerBase
{
    private readonly IReminderService _reminderService;

    public RemindersController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GetPending([FromQuery] DateTime? asOf, CancellationToken cancellationToken)
    {
        var reminders = await _reminderService.GetPendingRemindersAsync(asOf ?? DateTime.UtcNow, cancellationToken);
        return Ok(reminders);
    }

    [HttpPost]
    public async Task<ActionResult<ReminderDto>> Schedule([FromBody] CreateReminderRequest request, CancellationToken cancellationToken)
    {
        var reminder = await _reminderService.ScheduleReminderAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetPending), new { }, reminder);
    }

    [HttpPost("{reminderId:guid}/sent")]
    public async Task<IActionResult> MarkSent(Guid reminderId, CancellationToken cancellationToken)
    {
        await _reminderService.MarkAsSentAsync(reminderId, cancellationToken);
        return NoContent();
    }
}
