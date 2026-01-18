using FlowOps.Application.Interfaces;
using FlowOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowOps.Infrastructure.Persistence.Repositories;

public class WorkflowTemplateRepository : IWorkflowTemplateRepository
{
    private readonly FlowOpsDbContext _context;

    public WorkflowTemplateRepository(FlowOpsDbContext context)
    {
        _context = context;
    }

    public async Task<WorkflowTemplate?> GetByIdAsync(Guid id)
    {
        return await _context.WorkflowTemplates
            .Include(w => w.States)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}
