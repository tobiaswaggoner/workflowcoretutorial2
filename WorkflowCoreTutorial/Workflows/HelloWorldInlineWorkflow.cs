using WorkflowCore.Interface;

namespace WorkflowCoreTutorial.Workflows;

public class HelloWorldInlineWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder.StartWith(state =>
            {
                Console.WriteLine("Hello");
            })
            .Then(state =>
            {
                Console.WriteLine("World");
            });
    }

    public string Id => nameof(HelloWorldInlineWorkflow);
    public int Version => 1;
}