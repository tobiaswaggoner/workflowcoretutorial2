using WorkflowCore.Interface;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class ScheduleWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .StartWith<HelloStep>()
            .Schedule(_ => TimeSpan.FromSeconds(2))
                .Do(then => then.StartWith(_ => Console.WriteLine("Executed scheduled task!")))
            .Then<WorldStep>();
    }

    public string Id => nameof(ScheduleWorkflow);
    public int Version => 1;
}