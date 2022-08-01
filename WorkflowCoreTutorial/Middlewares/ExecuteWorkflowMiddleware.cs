using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Middlewares;

public class ExecuteWorkflowMiddleware : IWorkflowMiddleware
{
    public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
    {
        Console.WriteLine($"   +++ Executeworkflow Middleware executed for {workflow.WorkflowDefinitionId}");
        return next();
    }

    public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.ExecuteWorkflow;
}