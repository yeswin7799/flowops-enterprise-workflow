using FlowOps.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowOps.Application.Interfaces
{
    public interface IRequestRepository
    {
        Task<Request?> GetByIdAsync(Guid id);
        Task AddAsync(Request request);
        Task UpdateAsync(Request request);
    }
}
