using Automation.ControlCenter.Dashboard.Models.Processes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Automation.ControlCenter.Dashboard.Pages.Processes;

public class IndexModel : PageModel
{
    public int RunningCount { get; private set; }
    public int CompletedCount { get; private set; }
    public int FailedCount { get; private set; }

    public string? SelectedStatus { get; private set; }

    public List<ProcessListItemViewModel> Processes { get; private set; } = [];

    public void OnGet(string? status)
    {
        SelectedStatus = status;

        // Temporary in-memory data for UI development
        var allProcesses = GetMockProcesses();

        RunningCount = allProcesses.Count(p => p.Status == "Running");
        CompletedCount = allProcesses.Count(p => p.Status == "Completed");
        FailedCount = allProcesses.Count(p => p.Status == "Failed");

        Processes = string.IsNullOrEmpty(SelectedStatus)
            ? allProcesses
            : allProcesses
                .Where(p => p.Status == SelectedStatus)
                .ToList();
    }

    private static List<ProcessListItemViewModel> GetMockProcesses()
    {
        return
        [
            new() { Id = "1", Name = "Invoice Processing", Status = "Running", StartTime = DateTime.Now.AddMinutes(-5) },
            new() { Id = "2", Name = "Customer Sync", Status = "Completed", StartTime = DateTime.Now.AddMinutes(-40), Duration = TimeSpan.FromMinutes(35) },
            new() { Id = "3", Name = "Report Generation", Status = "Failed", StartTime = DateTime.Now.AddMinutes(-15) },
            new() { Id = "4", Name = "Email Dispatch", Status = "Completed", StartTime = DateTime.Now.AddMinutes(-60), Duration = TimeSpan.FromMinutes(10) }
        ];
    }
}
