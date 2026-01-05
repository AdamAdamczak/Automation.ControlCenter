using Automation.ControlCenter.Infrastructure.Persistence;
using Automation.ControlCenter.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Automation.ControlCenter.Dashboard.Pages.Processes;

public class IndexModel : PageModel
{
    private readonly ProcessDbContext _dbContext;

    public List<ProcessInstance> Processes { get; private set; } = [];

    public IndexModel(ProcessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnGetAsync()
    {
        Processes = await _dbContext.Processes
            .OrderByDescending(p => p.StartedAt)
            .ToListAsync();
    }
}
