using FlowOps.Domain.Enums;

namespace FlowOps.API.Models.Requests
{
    public class RequestStatusHistoryDto
    {
        public RequestStatus FromStatus { get; set; }

        public RequestStatus ToStatus { get; set; }

        public Guid ChangedByUserId { get; set; }

        public DateTime ChangedAtUtc { get; set; }

    }
}
