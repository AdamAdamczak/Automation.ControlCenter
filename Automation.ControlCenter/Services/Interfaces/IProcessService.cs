using Automation.ControlCenter.Domain;
using Automation.ControlCenter.DTOs;

namespace Automation.ControlCenter.Services.Interfaces;

public interface IProcessService
{
    StartProcessResponse StartProcess(StartProcessRequest request);
    ProcessStatus GetStatus(Guid processId);
    void UpdateStatus(Guid processId, ProcessStatus newStatus);
}
