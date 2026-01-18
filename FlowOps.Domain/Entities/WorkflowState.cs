using FlowOps.Domain.Common;
using FlowOps.Domain.Enums;

namespace FlowOps.Domain.Entities;

public class WorkflowState : BaseEntity
{
    public string Name { get; private set; }
    public int Order { get; private set; }
    public RequestStatus Status { get; private set; }

    public Guid WorkflowTemplateId { get; private set; }

    // 👇 NEW: allowed transitions
    private readonly List<RequestStatus> _allowedNextStatuses = new();
    public IReadOnlyCollection<RequestStatus> AllowedNextStatuses =>
        _allowedNextStatuses.AsReadOnly();

    private WorkflowState() { }

    public WorkflowState(
        string name,
        int order,
        RequestStatus status,
        Guid workflowTemplateId)
    {
        Name = name;
        Order = order;
        Status = status;
        WorkflowTemplateId = workflowTemplateId;
    }

    // 👇 NEW: define allowed transitions
    public void AllowTransitionTo(RequestStatus nextStatus)
    {
        if (!_allowedNextStatuses.Contains(nextStatus))
            _allowedNextStatuses.Add(nextStatus);
    }

    // 👇 NEW: validation method
    public bool CanTransitionTo(RequestStatus nextStatus)
    {
        return _allowedNextStatuses.Contains(nextStatus);
    }
}
