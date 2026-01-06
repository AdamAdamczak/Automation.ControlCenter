using Automation.ControlCenter.Domain;
using Automation.ControlCenter.DTOs;
using Automation.ControlCenter.DTOs.Dashboard;

namespace Automation.ControlCenter.Services.Interfaces;

public interface IProcessService
{
    StartProcessResponse StartProcess(StartProcessRequest request);
    ProcessStatus GetStatus(Guid processId);
    void UpdateStatus(Guid processId, ProcessStatus newStatus);
    DashboardOverviewDto GetDashboardOverview();
}
