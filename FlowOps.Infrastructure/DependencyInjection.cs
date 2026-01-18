using FlowOps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlowOps.Application.Interfaces;
using FlowOps.Infrastructure.Persistence.Repositories;

namespace FlowOps.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FlowOpsDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("FlowOpsDb")));
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();

        return services;
    }
}
