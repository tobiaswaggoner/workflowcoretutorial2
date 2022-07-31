using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Steps;

public class IncreaseCountStep : StepBody
{
    public int CounterIn { get; set; }
    public int CounterOut { get; set; }
    
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        CounterOut = CounterIn + 1;
        Console.WriteLine($"Incoming count {CounterIn} -> Outgoing count {CounterOut}");
        return ExecutionResult.Next();
    }
}