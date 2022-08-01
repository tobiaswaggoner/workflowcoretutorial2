using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class ForEachParallelWorkflow : IWorkflow<ForEachState>
{
    public void Build(IWorkflowBuilder<ForEachState> builder)
    {
        builder
            .ForEach(state => state.Names)
            .Do(next =>
                next
                    .StartWith<NameDumperStep>()
                    .Input(step => step.Name, (_, ctx) => (string) ctx.Item)
            )
            .Then(_ =>
            {
                Console.WriteLine("Loop done");
            });
    }

    public string Id => nameof(ForEachParallelWorkflow);
    public int Version => 1;
}