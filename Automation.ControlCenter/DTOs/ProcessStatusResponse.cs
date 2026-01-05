namespace Automation.ControlCenter.DTOs;

public class ProcessStatusResponse
{
    public Guid ProcessId { get; set; }
    public string Status { get; set; } = string.Empty;
}
