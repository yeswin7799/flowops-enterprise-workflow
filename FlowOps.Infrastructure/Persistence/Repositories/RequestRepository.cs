using FlowOps.Application.Interfaces;
using FlowOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowOps.Infrastructure.Persistence.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly FlowOpsDbContext _context;

    public RequestRepository(FlowOpsDbContext context)
    {
        _context = context;
    }

    public async Task<Request?> GetByIdAsync(Guid id)
    {
        return await _context.Requests
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Request request)
    {
        await _context.Requests.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Request request)
    {
        _context.Requests.Update(request);
        await _context.SaveChangesAsync();
    }
}
