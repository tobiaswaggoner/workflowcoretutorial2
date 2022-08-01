using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Steps;

public class DumpCounterStep : StepBody
{
    public int CurrentCounter { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine($"Current Counter is {CurrentCounter}");
        return ExecutionResult.Next();
    }
}