using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Infrastructure;

public class InMemoryProcessStore
{
    private readonly Dictionary<Guid, ProcessInstance> _processes = new();

    public void Add(ProcessInstance process)
    {
        _processes[process.Id] = process;
    }

    public ProcessInstance? Get(Guid id)
    {
        _processes.TryGetValue(id, out var process);
        return process;
    }
}
