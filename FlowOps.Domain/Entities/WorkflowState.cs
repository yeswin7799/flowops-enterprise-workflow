using FlowOps.Domain.Common;

namespace FlowOps.Domain.Entities;

public class WorkflowState : BaseEntity
{
    public string Name { get; private set; }
    public int Order { get; private set; }

    public Guid WorkflowTemplateId { get; private set; }

    private WorkflowState() { }

    public WorkflowState(string name, int order, Guid workflowTemplateId)
    {
        Name = name;
        Order = order;
        WorkflowTemplateId = workflowTemplateId;
    }
}
