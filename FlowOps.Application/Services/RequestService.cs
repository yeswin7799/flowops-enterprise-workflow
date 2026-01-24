using FlowOps.Application.Interfaces;
using FlowOps.Domain.Entities;
using FlowOps.Domain.Enums;

namespace FlowOps.Application.Services;

public class RequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IWorkflowTemplateRepository _workflowTemplateRepository;


    public RequestService(
    IRequestRepository requestRepository,
    IWorkflowTemplateRepository workflowTemplateRepository)
    {
        _requestRepository = requestRepository;
        _workflowTemplateRepository = workflowTemplateRepository;
    }


    public async Task<Guid> CreateRequestAsync(
    string title,
    string description,
    RequestPriority priority,
    Guid createdByUserId,
    Guid workflowTemplateId)
    {
        var workflowTemplate =
            await _workflowTemplateRepository.GetByIdAsync(workflowTemplateId)
            ?? throw new InvalidOperationException("Workflow template not found");

        var request = new Request(
            title,
            description,
            priority,
            workflowTemplateId,
            createdByUserId
        );

        await _requestRepository.AddAsync(request);

        return request.Id;
    }

    public async Task<Request?> GetByIdAsync(Guid id)
    {
        var request = await _requestRepository.GetByIdWithHistoryAsync(id);

        if (request == null)
            throw new InvalidOperationException("Request not found");

        return request;
    }


    public async Task SubmitRequestAsync(
    Guid requestId,
    Guid changedByUserId)
    {
        var request =
            await _requestRepository.GetByIdAsync(requestId)
            ?? throw new InvalidOperationException("Request not found");

        var workflowTemplate =
            await _workflowTemplateRepository.GetByIdAsync(request.WorkflowTemplateId)
            ?? throw new InvalidOperationException("Workflow template not found");

        var currentState =
            workflowTemplate.GetStateByStatus(request.Status)
            ?? throw new InvalidOperationException("Invalid workflow state");

        // Move request to next workflow status
        var nextState = workflowTemplate.GetNextState(currentState);

        request.ChangeStatus(
            currentState,
            nextState.Status,
            changedByUserId
        );

        await _requestRepository.UpdateAsync(request);
    }



}
