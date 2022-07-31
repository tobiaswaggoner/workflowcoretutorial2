using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCoreTutorial.Services;

namespace WorkflowCoreTutorial.Steps;

public class DumpTimeStampStep : StepBody
{
    private readonly TimestampService _timestampService;

    public DumpTimeStampStep(TimestampService timestampService)
    {
        _timestampService = timestampService;
    }
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine($"Current time stamp: {_timestampService.TimeStamp}");
        return ExecutionResult.Next();
    }
}