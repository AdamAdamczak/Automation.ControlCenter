namespace Automation.ControlCenter.DTOs;

public class StartProcessRequest
{
    public string ProcessName { get; set; } = string.Empty;
    public string TriggeredBy { get; set; } = string.Empty;
}
