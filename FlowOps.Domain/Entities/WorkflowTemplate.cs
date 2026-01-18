using FlowOps.Domain.Common;
using FlowOps.Domain.Enums;

namespace FlowOps.Domain.Entities;

public class WorkflowTemplate : BaseEntity
{
    private readonly List<WorkflowState> _states = new();

    public string Name { get; private set; }

    public IReadOnlyCollection<WorkflowState> States =>
        _states.AsReadOnly();

    private WorkflowTemplate() { }

    public WorkflowTemplate(string name)
    {
        Name = name;
    }

    public void AddState(WorkflowState state)
    {
        _states.Add(state);
    }

    public WorkflowState GetInitialState()
    {
        return _states
            .OrderBy(s => s.Order)
            .First();
    }

    public WorkflowState? GetStateByStatus(RequestStatus status)
    {
        return _states
            .FirstOrDefault(s => s.Status == status);
    }

    public WorkflowState GetNextState(WorkflowState currentState)
    {
        var nextState = _states
            .FirstOrDefault(s => s.Order == currentState.Order + 1);

        if (nextState is null)
            throw new InvalidOperationException("No next workflow state");

        return nextState;
    }
}
