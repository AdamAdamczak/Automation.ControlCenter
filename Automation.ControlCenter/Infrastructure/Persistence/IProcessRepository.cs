using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Infrastructure.Persistence;

public interface IProcessRepository
{
    void Add(ProcessInstance process);
    ProcessInstance? Get(Guid id);
    void Update(ProcessInstance process);
}
