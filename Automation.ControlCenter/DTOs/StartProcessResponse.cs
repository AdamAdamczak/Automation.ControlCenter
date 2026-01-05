namespace Automation.ControlCenter.DTOs;

public class StartProcessResponse
{
    public Guid ProcessId { get; set; }
    public string Status { get; set; } = string.Empty;
}
