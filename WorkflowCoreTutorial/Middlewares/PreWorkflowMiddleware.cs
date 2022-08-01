using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Middlewares;

public class PreWorkflowMiddleware : IWorkflowMiddleware
{
    public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
    {
        Console.WriteLine($"   +++ Preworkflow Middleware executed for {workflow.WorkflowDefinitionId}");
        return next();
    }

    public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PreWorkflow;
}