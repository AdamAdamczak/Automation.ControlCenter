using Automation.ControlCenter.Models;

namespace Automation.ControlCenter.Domain;

public interface IProcessRepository
{
    void Add(ProcessInstance process);
    ProcessInstance? Get(Guid id);
    void Update(ProcessInstance process);
    IEnumerable<ProcessInstance> GetAll();
}
