using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Steps;

public class BranchAStep1 : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("We are on Branch A!");
        return ExecutionResult.Next();
    }
}
