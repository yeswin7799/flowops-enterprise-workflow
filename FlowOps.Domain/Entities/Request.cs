using FlowOps.Domain.Common;
using FlowOps.Domain.Enums;

namespace FlowOps.Domain.Entities;

public class Request : BaseEntity
{
    private readonly List<RequestStatusHistory> _statusHistory = new();

    public string Title { get; private set; }
    public string Description { get; private set; }
    public RequestPriority Priority { get; private set; }

    public RequestStatus Status { get; private set; }

    public Guid CreatedByUserId { get; private set; }
    public Guid? AssignedToUserId { get; private set; }
    public Guid WorkflowTemplateId { get; private set; }


    public IReadOnlyCollection<RequestStatusHistory> StatusHistory =>
        _statusHistory.AsReadOnly();

    private Request() { }

    public Request(
        string title,
        string description,
        RequestPriority priority,
        Guid workflowTemplateId,
        Guid createdByUserId)
    {
        Title = title;
        Description = description;
        Priority = priority;

        WorkflowTemplateId = workflowTemplateId;
        CreatedByUserId = createdByUserId;

        Status = RequestStatus.Draft;

        _statusHistory.Add(
            RequestStatusHistory.Create(
                Id,
                Status,
                Status,
                createdByUserId
            )
        );
    }

    // 🔒 ONLY valid way to change status
    public void ChangeStatus(
        WorkflowState currentState,
        RequestStatus newStatus,
        Guid changedByUserId)
    {
        if (!currentState.CanTransitionTo(newStatus))
            throw new InvalidOperationException(
                $"Transition from {Status} to {newStatus} is not allowed.");

        var previousStatus = Status;
        Status = newStatus;

        _statusHistory.Add(
            RequestStatusHistory.Create(
                Id,
                previousStatus,
                newStatus,
                changedByUserId
            )
        );

        MarkUpdated();
    }

    public void Assign(Guid userId)
    {
        AssignedToUserId = userId;
        MarkUpdated();
    }
}
