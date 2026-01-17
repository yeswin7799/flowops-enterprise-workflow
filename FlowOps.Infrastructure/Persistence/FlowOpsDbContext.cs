using FlowOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FlowOps.Infrastructure.Persistence;

public class FlowOpsDbContext : DbContext
{
    public FlowOpsDbContext(DbContextOptions<FlowOpsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Request> Requests => Set<Request>();
    public DbSet<WorkflowTemplate> WorkflowTemplates => Set<WorkflowTemplate>();
    public DbSet<WorkflowState> WorkflowStates => Set<WorkflowState>();
    public DbSet<RequestStatusHistory> RequestStatusHistories => Set<RequestStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowOpsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
