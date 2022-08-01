using WorkflowCore.Interface;

namespace WorkflowCoreTutorial.Workflows;

public class ExternalEventWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .StartWith(_ => Console.WriteLine("Workflow is waiting for reboot"))
            .WaitFor("Rebooted", _ => string.Empty)
            .Then(_ => Console.WriteLine("Reboot occurred"));
    }

    public string Id => nameof(ExternalEventWorkflow);
    public int Version => 1;
}