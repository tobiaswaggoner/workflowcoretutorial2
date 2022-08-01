using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreTutorial.Steps;

public class NameDumperStep : StepBody
{
    public string Name { get; set; }
    
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Thread.Sleep(new Random().Next(10, 500));
        Console.WriteLine($"{Name} was given to me");
        return ExecutionResult.Next();
    }
}