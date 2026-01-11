using FlowOps.Domain.Common;

namespace FlowOps.Domain.Entities;

public class WorkflowTemplate : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly List<WorkflowState> _states = new();
    public IReadOnlyCollection<WorkflowState> States => _states.AsReadOnly();

    private WorkflowTemplate() { } // EF Core

    public WorkflowTemplate(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddState(WorkflowState state)
    {
        _states.Add(state);
        MarkUpdated();
    }
}
