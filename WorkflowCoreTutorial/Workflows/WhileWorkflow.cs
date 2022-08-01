using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class WhileWorkflow : IWorkflow<CounterState>
{
    public void Build(IWorkflowBuilder<CounterState> builder)
    {
        builder
            .While(state => state.CurrentCount < 5)
            .Do(then =>
                then
                    .Then<IncreaseCountStep>()
                        .Input( step => step.CounterIn, state => state.CurrentCount )
                        .Output( state => state.CurrentCount, step => step.CounterOut )
            );
    }

    public string Id => nameof(WhileWorkflow);
    public int Version => 1;
}