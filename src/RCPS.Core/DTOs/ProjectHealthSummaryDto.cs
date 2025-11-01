using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ProjectHealthSummaryDto(
    ProjectHealth Health,
    int Count);
