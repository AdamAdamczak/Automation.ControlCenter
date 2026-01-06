using Automation.ControlCenter.Infrastructure.Exceptions;
using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Domain;

/// <summary>
/// Applies validated status transitions to a process instance.
/// </summary>
public class ProcessStateService
{
    public void ChangeStatus(ProcessInstance process, ProcessStatus newStatus)
    {
        if (!ProcessStatusRules.CanTransition(process.Status, newStatus))
        {
            throw new InvalidStatusTransitionException(
                process.Status,
                newStatus);
        }

        process.Status = newStatus;
    }
}
