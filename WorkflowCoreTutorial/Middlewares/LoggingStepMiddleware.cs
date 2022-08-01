using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Middlewares;

public class LoggingStepMiddleware : IWorkflowStepMiddleware
{
    public Task<ExecutionResult> HandleAsync(IStepExecutionContext context, IStepBody body, WorkflowStepDelegate next)
    {
        Console.WriteLine($"  .... now executing {context.Workflow.WorkflowDefinitionId}.{context.Step.Id}-{body.GetType().Name}");
        return next();
    }
}