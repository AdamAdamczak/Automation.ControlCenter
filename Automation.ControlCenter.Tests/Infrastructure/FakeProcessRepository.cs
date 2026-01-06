using Automation.ControlCenter.Domain;
using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Tests.Infrastructure;

public class FakeProcessRepository : IProcessRepository
{
    private readonly Dictionary<Guid, ProcessInstance> _store = new();

    public void Add(ProcessInstance process)
    {
        _store[process.Id] = process;
    }

    public ProcessInstance? Get(Guid id)
    {
        _store.TryGetValue(id, out var process);
        return process;
    }

    public void Update(ProcessInstance process)
    {
        _store[process.Id] = process;
    }
}
