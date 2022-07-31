using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class CounterWorkflow : IWorkflow<CounterState>
{
    public void Build(IWorkflowBuilder<CounterState> builder)
    {
        builder
            .StartWith<IncreaseCountStep>()
                .Input(step => step.CounterIn, state => state.CurrentCount)
                .Output(state => state.CurrentCount, step => step.CounterOut)
            .Then<IncreaseCountStep>()
                .Input(step => step.CounterIn, state => state.CurrentCount);
    }

    public string Id => nameof(CounterWorkflow);
    public int Version => 1;
}