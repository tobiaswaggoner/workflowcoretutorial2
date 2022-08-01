using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class ExternalActivityWorkflow : IWorkflow<CounterState>
{
    public void Build(IWorkflowBuilder<CounterState> builder)
    {
        builder
            .StartWith<DumpCounterStep>()
                .Input(step => step.CurrentCounter, state => state.CurrentCount)
            .Then(_ => Console.WriteLine("About to trigger external activity"))
            .Activity("IncreaseCounter", state => state.CurrentCount)
                .Output(state => state.CurrentCount, activity => (int) activity.Result)
            .Then<DumpCounterStep>()
                .Input(step => step.CurrentCounter, state => state.CurrentCount);
    }

    public string Id => nameof(ExternalActivityWorkflow);
    public int Version => 1;
}