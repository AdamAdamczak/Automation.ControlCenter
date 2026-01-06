using Automation.ControlCenter.Domain;

namespace Automation.ControlCenter.DTOs.Dashboard;

/// <summary>
/// Read model for dashboard overview screen.
/// </summary>
public class DashboardOverviewDto
{
    public int QueuedCount { get; set; }
    public int RunningCount { get; set; }
    public int CompletedCount { get; set; }
    public int FailedCount { get; set; }

    public List<ProcessListItemDto> Processes { get; set; } = [];
}
