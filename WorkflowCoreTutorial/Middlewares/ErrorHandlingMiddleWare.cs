using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Middlewares;

public class ErrorHandlingMiddleWare : IWorkflowStepMiddleware
{
    public Task<ExecutionResult> HandleAsync(IStepExecutionContext context, IStepBody body, WorkflowStepDelegate next)
    {
        try
        {
            return next();
        }
        catch (Exception e)
        {
            Console.WriteLine( $"   ... middlware logged exception : {e.Message}");
            throw;
        }
    }
}