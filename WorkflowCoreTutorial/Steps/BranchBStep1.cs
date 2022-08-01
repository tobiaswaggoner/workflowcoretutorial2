using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Steps;

public class BranchBStep1 : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("We are on Branch B!");
        return ExecutionResult.Next();
    }
}
