using Automation.ControlCenter.Domain;

namespace Automation.ControlCenter.Dashboard.DTOs.Dashboard;

/// <summary>
/// Single process row for dashboard process list.
/// </summary>
public class ProcessListItemDto
{

    public Guid ProcessId { get; set; }
    public string ProcessName { get; set; } = string.Empty;
    public ProcessStatus Status { get; set; }
    public DateTime StartedAt { get; set; }
}
