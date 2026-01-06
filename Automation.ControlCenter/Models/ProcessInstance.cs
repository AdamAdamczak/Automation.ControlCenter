using Automation.ControlCenter.Domain;

namespace Automation.ControlCenter.Models;

public class ProcessInstance
{
    public Guid Id { get; set; }
    public string ProcessName { get; set; } = string.Empty;
    public ProcessStatus Status { get; set; }
    public DateTime StartedAt { get; set; }
}
