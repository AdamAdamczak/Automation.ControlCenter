using Automation.ControlCenter.Domain;

namespace Automation.ControlCenter.Infrastructure.Exceptions;

public class InvalidStatusTransitionException : Exception
{
    public InvalidStatusTransitionException(
        ProcessStatus current,
        ProcessStatus next)
        : base($"Cannot change status from {current} to {next}")
    {
    }
}
