using Automation.ControlCenter.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Automation.ControlCenter.Infrastructure.Persistence;

public class ProcessDbContext : DbContext
{
    public DbSet<ProcessInstance> Processes => Set<ProcessInstance>();

    public ProcessDbContext(DbContextOptions<ProcessDbContext> options)
        : base(options)
    {
    }
}
