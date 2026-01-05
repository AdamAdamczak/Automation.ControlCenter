namespace Automation.ControlCenter.Infrastructure.Exceptions;

public class ProcessNotFoundException : Exception
{
    public ProcessNotFoundException(Guid processId)
        : base($"Process with id '{processId}' was not found")
    {
    }
}
