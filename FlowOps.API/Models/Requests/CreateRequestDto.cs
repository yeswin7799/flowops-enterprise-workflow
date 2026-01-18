using FlowOps.Domain.Enums;

namespace FlowOps.API.Models.Requests;

public class CreateRequestDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public RequestPriority Priority { get; set; }
    public Guid WorkflowTemplateId { get; set; }
    public Guid CreatedByUserId { get; set; }
}
