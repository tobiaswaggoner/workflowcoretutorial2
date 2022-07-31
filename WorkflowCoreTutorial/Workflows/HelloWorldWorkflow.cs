using WorkflowCore.Interface;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class HelloWorldWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .StartWith<HelloStep>()
            .Then<WorldStep>();
    }

    public string Id => nameof(HelloWorldWorkflow);
    public int Version => 1;
}