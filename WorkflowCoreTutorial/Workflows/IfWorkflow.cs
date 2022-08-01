using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class IfWorkflow : IWorkflow<IfState>
{
    public void Build(IWorkflowBuilder<IfState> builder)
    {
        builder
            .StartWith<HelloStep>()
            .If(state => state.EnterIf)
                .Do(then => then.Then<IfStep>())
            .Then<WorldStep>();
    }

    public string Id => nameof(IfWorkflow);
    public int Version => 1;
}