using Automation.ControlCenter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IProcessService _processService;

    public DashboardController(IProcessService processService)
    {
        _processService = processService;
    }

    [HttpGet("overview")]
    public IActionResult GetOverview()
    {
        var overview = _processService.GetDashboardOverview();
        return Ok(overview);
    }
}
