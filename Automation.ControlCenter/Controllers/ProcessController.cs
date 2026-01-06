using Automation.ControlCenter.Domain;
using Automation.ControlCenter.DTOs;
using Automation.ControlCenter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automation.ControlCenter.Controllers;

[ApiController]
[Route("api/process")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _processService;

    public ProcessController(IProcessService processService)
    {
        _processService = processService;
    }

    [HttpPost("start")]
    public ActionResult<StartProcessResponse> Start(StartProcessRequest request)
    {
        var result = _processService.StartProcess(request);
        return Ok(result);
    }

    [HttpGet("{id}/status")]
    public ActionResult<ProcessStatusResponse> GetStatus(Guid id)
    {
        var status = _processService.GetStatus(id);

        return Ok(new ProcessStatusResponse
        {
            ProcessId = id,
            Status = status.ToString(),
        });
    }

    [HttpPost("{id}/status")]
    public IActionResult UpdateStatus(Guid id, UpdateProcessStatusRequest request)
    {
        if (!Enum.TryParse<ProcessStatus>(
            request.Status,
            ignoreCase: true,
            out var newStatus))
        {
            return BadRequest("Invalid status value");
        }

        _processService.UpdateStatus(id, newStatus);
        return NoContent();
    }

}
