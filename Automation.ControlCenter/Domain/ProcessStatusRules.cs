using Automation.ControlCenter.Domain;

namespace Automation.ControlCenter.Domain;

/// <summary>
/// Central place defining allowed process status transitions.
/// </summary>
public static class ProcessStatusRules
{
    public static bool CanTransition(ProcessStatus current, ProcessStatus next)
    {
        return (current, next) switch
        {
            (ProcessStatus.Queued, ProcessStatus.Running) => true,

            (ProcessStatus.Running, ProcessStatus.Completed) => true,
            (ProcessStatus.Running, ProcessStatus.Failed) => true,
            (ProcessStatus.Running, ProcessStatus.TimedOut) => true,

            _ => false
        };
    }
}
