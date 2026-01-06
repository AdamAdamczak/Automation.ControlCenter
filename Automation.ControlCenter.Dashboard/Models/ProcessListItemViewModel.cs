namespace Automation.ControlCenter.Dashboard.Models.Processes;

public class ProcessListItemViewModel
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public TimeSpan? Duration { get; init; }
}
