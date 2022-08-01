using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class RecurWorkflow : IWorkflow<CounterState>
{
    public void Build(IWorkflowBuilder<CounterState> builder)
    {
        builder
            .StartWith<HelloStep>()
            .Recur(_ => TimeSpan.FromMilliseconds(500), state => state.CurrentCount >= 5)
                .Do(then => 
                then
                    .StartWith(_ => Console.WriteLine("Recur step executed"))
                    .Then<IncreaseCountStep>()
                        .Input( step => step.CounterIn, state => state.CurrentCount )
                        .Output( state => state.CurrentCount, step => step.CounterOut)
                )
            .Then<WorldStep>();
    }

    public string Id => nameof(RecurWorkflow);
    public int Version => 1;
}