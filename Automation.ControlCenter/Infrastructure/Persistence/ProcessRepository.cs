using Automation.ControlCenter.Domain;
using Automation.ControlCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace Automation.ControlCenter.Infrastructure.Persistence;

public class ProcessRepository : IProcessRepository
{
    private readonly ProcessDbContext _context;

    public ProcessRepository(ProcessDbContext context)
    {
        _context = context;
    }

    public void Add(ProcessInstance process)
    {
        _context.Processes.Add(process);
        _context.SaveChanges();
    }

    public ProcessInstance? Get(Guid id)
    {
        return _context.Processes
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);
    }

    public void Update(ProcessInstance process)
    {
        _context.Processes.Update(process);
        _context.SaveChanges();
    }
}
