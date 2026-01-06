using Automation.ControlCenter.Domain;
using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Tests.Infrastructure;

public class FakeProcessRepository : IProcessRepository
{
    private readonly Dictionary<Guid, ProcessInstance> _store = new();
    private readonly List<ProcessInstance> _processes = new();

    public ProcessInstance? Get(Guid id)
    {
        return _processes.FirstOrDefault(p => p.Id == id);
    }
    public void Add(ProcessInstance process)
    {
        _store[process.Id] = process;
    }

    public void Update(ProcessInstance process)
    {
        _store[process.Id] = process;
    }
    public IEnumerable<ProcessInstance> GetAll()
    {
        return _processes;
    }
}
