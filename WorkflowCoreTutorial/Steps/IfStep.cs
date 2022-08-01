using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Steps;

public class IfStep : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("I am inside the if block!");
        return ExecutionResult.Next();
    }
}