using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Middlewares;

public class PostWorkflowMiddleware : IWorkflowMiddleware
{
    public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
    {
        Console.WriteLine($"   +++ Postworkflow Middleware executed for {workflow.WorkflowDefinitionId}");
        return next();
    }

    public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PostWorkflow;
}