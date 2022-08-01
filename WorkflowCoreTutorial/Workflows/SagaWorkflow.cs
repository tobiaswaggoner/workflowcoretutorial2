using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Workflows;

public class SagaWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .Saga(saga =>
                saga
                    .StartWith(_ => Console.WriteLine("1. step"))
                    .CompensateWith(_ => Console.WriteLine("Undo 1. step"))
                    .Then(_ => Console.WriteLine("2. step"))
                    .CompensateWith(_ => Console.WriteLine("Undo 2. step"))
                    .Then(_ =>
                    {
                        Console.WriteLine("3. step. Throws an exception");
                        throw new ApplicationException("Bad exception");
                    })
                    .CompensateWith(_ => Console.WriteLine("Undo 3. step"))
                    .Then(_ => Console.WriteLine("4. step"))
                    .CompensateWith(_ => Console.WriteLine("Undo 4. step"))
            )
            .OnError(WorkflowErrorHandling.Compensate)
            .CompensateWith( _ => Console.WriteLine("Catch -- an error occured during saga"))
            .Then(_ => Console.WriteLine("Workflow finished"));
    }

    public string Id => nameof(SagaWorkflow);
    public int Version => 1;
}