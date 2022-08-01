using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Workflows;

public class ErrorHandlingWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .StartWith(_ =>
            {
                Console.WriteLine("An error is about to happen");
                throw new ApplicationException("A terrible exception just happned");
            })
            .OnError(WorkflowErrorHandling.Compensate)
            .CompensateWith(_ =>
            {
                Console.WriteLine("We have to compensate for this error");
            });
    }

    public string Id => nameof(ErrorHandlingWorkflow);
    public int Version => 1;
}