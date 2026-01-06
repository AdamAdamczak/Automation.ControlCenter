using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Automation.ControlCenter.Dashboard.Pages.Processes;

public class IndexModel : PageModel
{
    public int RunningCount { get; private set; }
    public int CompletedCount { get; private set; }
    public int FailedCount { get; private set; }
    public string? SelectedStatus { get; private set; }
    public void OnGet(string? status)
    {
        // Temporary hardcoded values for UI testing
        RunningCount = 2;
        CompletedCount = 5;
        FailedCount = 1;
        // Selected status from query string
        SelectedStatus = status;
    }
}
