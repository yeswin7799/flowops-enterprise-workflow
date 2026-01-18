using FlowOps.Domain.Entities;

namespace FlowOps.Application.Interfaces;

public interface IWorkflowTemplateRepository
{
    Task<WorkflowTemplate?> GetByIdAsync(Guid id);
}
