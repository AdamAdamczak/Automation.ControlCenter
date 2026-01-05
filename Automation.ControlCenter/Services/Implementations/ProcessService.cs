using Automation.ControlCenter.DTOs;
using Automation.ControlCenter.Infrastructure;
using Automation.ControlCenter.Infrastructure.Exceptions;
using Automation.ControlCenter.Infrastructure.Persistence;
using Automation.ControlCenter.Models;
using Automation.ControlCenter.Services.Interfaces;

namespace Automation.ControlCenter.Services.Implementations;

public class ProcessService : IProcessService
{
    private readonly IProcessRepository _repository;
    private readonly ILogger<ProcessService> _logger;
    public ProcessService(
        IProcessRepository repository,
        ILogger<ProcessService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public StartProcessResponse StartProcess(StartProcessRequest request)
    {
        var process = new ProcessInstance
        {
            Id = Guid.NewGuid(),
            ProcessName = request.ProcessName,
            Status = ProcessStatus.Started,
            StartedAt = DateTime.UtcNow
        };

        _repository.Add(process);
        _logger.LogInformation(
            "Process started. ProcessId={ProcessId}, Name={ProcessName}",
            process.Id,
            process.ProcessName);

        return new StartProcessResponse
        {
            ProcessId = process.Id,
            Status = process.Status.ToString()
        };
    }

    public ProcessStatus GetStatus(Guid processId)
    {
        var process = _repository.Get(processId);

        if (process == null)
        {
            throw new ProcessNotFoundException(processId);
        }
        _logger.LogInformation(
            "Status requested. ProcessId={ProcessId}, Status={Status}",
            process.Id,
            process.Status);

        return process.Status;
    }

    public void UpdateStatus(Guid processId, ProcessStatus newStatus)
    {
        var process = _repository.Get(processId);

        if (process == null)
        {
            throw new ProcessNotFoundException(processId);
        }

        if (!IsTransitionAllowed(process.Status, newStatus))
        {
            _logger.LogWarning(
                "Invalid status transition. ProcessId={ProcessId}, From={From}, To={To}",
                process.Id,
                process.Status,
                newStatus);
            throw new InvalidStatusTransitionException(
            process.Status,
            newStatus);
        }

        process.Status = newStatus;
    }
    private static bool IsTransitionAllowed(ProcessStatus current, ProcessStatus next)
    {
        return current switch
        {
            ProcessStatus.Started => next == ProcessStatus.Running,
            ProcessStatus.Running => next is ProcessStatus.Completed or ProcessStatus.Failed,
            _ => false
        };
    }
}




