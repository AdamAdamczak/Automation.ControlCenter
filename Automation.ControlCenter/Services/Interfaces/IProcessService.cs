using Automation.ControlCenter.DTOs;
using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Services.Interfaces;

public interface IProcessService
{
    StartProcessResponse StartProcess(StartProcessRequest request);
    ProcessStatus GetStatus(Guid processId);
    void UpdateStatus(Guid processId, ProcessStatus newStatus);
}
