using FlowOps.Domain.Common;
using FlowOps.Domain.Enums;

namespace FlowOps.Domain.Entities
{
    public class Request : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public RequestPriority Priority { get; private set; }
        public RequestStatus Status { get; private set; }

        public Guid CreatedByUserId { get; private set; }
        public Guid? AssignedToUserId { get; private set; }

        private Request() { }

        public Request(
        string title,
        string description,
        RequestPriority priority,
        Guid createdByUserId)
        {
            Title = title;
            Description = description;
            Priority = priority;
            Status = RequestStatus.Draft;
            CreatedByUserId = createdByUserId;
        }

        public void Submit()
        {
            if (Status != RequestStatus.Draft)
                throw new InvalidOperationException("Only draft requests can be submitted.");
            Status = RequestStatus.Submitted;
            MarkUpdated();
        }

        public void Assign(Guid userId)
        {
            AssignedToUserId = userId;
            MarkUpdated();
        }
        public void ChangeStatus(RequestStatus newStatus)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
            MarkUpdated();
        }
    }
}
