using FlowOps.Domain.Enums;

namespace FlowOps.API.Models.Requests
{
    public class RequestDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public RequestPriority Priority { get; set; }
        public RequestStatus Status { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public List<RequestStatusHistoryDto> StatusHistory { get; set; } = new();
    }
}
