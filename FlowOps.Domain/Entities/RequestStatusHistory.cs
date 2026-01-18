using FlowOps.Domain.Common;
using FlowOps.Domain.Enums;

namespace FlowOps.Domain.Entities;

public class RequestStatusHistory : BaseEntity
{
    public Guid RequestId { get; private set; }
    public RequestStatus FromStatus { get; private set; }
    public RequestStatus ToStatus { get; private set; }
    public Guid ChangedByUserId { get; private set; }
    public DateTime ChangedAtUtc { get; private set; }

    private RequestStatusHistory() { }

    private RequestStatusHistory(
        Guid requestId,
        RequestStatus fromStatus,
        RequestStatus toStatus,
        Guid changedByUserId)
    {
        RequestId = requestId;
        FromStatus = fromStatus;
        ToStatus = toStatus;
        ChangedByUserId = changedByUserId;
        ChangedAtUtc = DateTime.UtcNow;
    }

    // ✅ Factory method (THIS is what we use)
    public static RequestStatusHistory Create(
        Guid requestId,
        RequestStatus fromStatus,
        RequestStatus toStatus,
        Guid changedByUserId)
    {
        return new RequestStatusHistory(
            requestId,
            fromStatus,
            toStatus,
            changedByUserId
        );
    }
}
