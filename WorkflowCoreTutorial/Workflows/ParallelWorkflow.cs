using WorkflowCore.Interface;

namespace WorkflowCoreTutorial.Workflows;

public class ParallelWorkflow : IWorkflow<object>
{
    public void Build(IWorkflowBuilder<object> builder)
    {
        builder.Parallel()
            .Do(then => then.StartWith(_ =>
            {
                var waitTime = new Random().Next(0, 500);
                Console.WriteLine($"Parallel Job 1: Waiting {waitTime}");
                Thread.Sleep(waitTime);
                Console.WriteLine("Parallel Job 1");
            }))
            .Do(then => then.StartWith(_ =>
            {
                var waitTime = new Random().Next(0, 500);
                Console.WriteLine($"Parallel Job 2: Waiting {waitTime}");
                Thread.Sleep(waitTime);
                Console.WriteLine("Parallel Job 2");
            }))
            .Do(then => then.StartWith(_ =>
                {
                    var waitTime = new Random().Next(0, 500);
                    Console.WriteLine($"Parallel Job 3: Waiting {waitTime}");
                    Thread.Sleep(waitTime);
                    Console.WriteLine("Parallel Job 3");
                    ;
                })
            )
            .Join()
            .Then(_ => Console.WriteLine("Parallel workflow finished"));
    }

    public string Id => nameof(ParallelWorkflow);
    public int Version => 1;
}