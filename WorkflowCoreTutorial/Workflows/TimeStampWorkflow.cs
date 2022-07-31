using WorkflowCore.Interface;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class TimeStampWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder.StartWith<DumpTimeStampStep>();
    }

    public string Id => nameof(TimeStampWorkflow);
    public int Version => 1;
}