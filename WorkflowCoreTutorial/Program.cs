using System.Diagnostics.Metrics;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCoreTutorial.Middlewares;
using WorkflowCoreTutorial.Services;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;
using WorkflowCoreTutorial.Workflows;

IServiceCollection services = new ServiceCollection();
services.AddLogging();
services.AddWorkflow();
services.AddTransient<TimestampService>();
services.AddTransient<DumpTimeStampStep>();
services.AddWorkflowStepMiddleware<ErrorHandlingMiddleWare>();
services.AddWorkflowStepMiddleware<LoggingStepMiddleware>();
services.AddWorkflowMiddleware<PreWorkflowMiddleware>();
services.AddWorkflowMiddleware<PostWorkflowMiddleware>();
services.AddWorkflowMiddleware<ExecuteWorkflowMiddleware>();

var serviceProvider = services.BuildServiceProvider();
var host = serviceProvider.GetService<IWorkflowHost>()!;

host.RegisterWorkflow<HelloWorldInlineWorkflow, object>();
host.RegisterWorkflow<HelloWorldWorkflow, object>();
host.RegisterWorkflow<CounterWorkflow, CounterState>();
host.RegisterWorkflow<TimeStampWorkflow, object>();
host.RegisterWorkflow<BranchingWorkflow, BranchingState>();
host.RegisterWorkflow<IfWorkflow, IfState>();
host.RegisterWorkflow<WhileWorkflow, CounterState>();
host.RegisterWorkflow<ForEachParallelWorkflow, ForEachState>();
host.RegisterWorkflow<ParallelWorkflow, object>();
host.RegisterWorkflow<ScheduleWorkflow, object>();
host.RegisterWorkflow<RecurWorkflow, CounterState>();
host.RegisterWorkflow<ErrorHandlingWorkflow, object>();
host.RegisterWorkflow<SagaWorkflow, object>();
host.RegisterWorkflow<ExternalEventWorkflow, object>();
host.RegisterWorkflow<ExternalActivityWorkflow, CounterState>();

host.Start(); 

var workflowInstanceId = await host.StartWorkflow(nameof(HelloWorldInlineWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(HelloWorldWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(CounterWorkflow), new CounterState { CurrentCount = 4});
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(TimeStampWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(BranchingWorkflow), new BranchingState { Branch = "B"});
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(IfWorkflow), new IfState { EnterIf = true});
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(IfWorkflow), new IfState { EnterIf = false});
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(WhileWorkflow), new CounterState { CurrentCount = 1});
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(ForEachParallelWorkflow), new ForEachState());
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(ParallelWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(ScheduleWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(RecurWorkflow), new CounterState { CurrentCount = 1});
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(ErrorHandlingWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(SagaWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(ExternalEventWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

await Task.Run(() =>
{
    Console.WriteLine(".... waiting for reboot");
    Thread.Sleep(4000);
    Console.WriteLine(".... reboot finished");
    host.PublishEvent("Rebooted", string.Empty, null);
});

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);

workflowInstanceId = await host.StartWorkflow(nameof(ExternalActivityWorkflow));
Console.WriteLine($"{workflowInstanceId} workflow started");

var activity = await host.GetPendingActivity("IncreaseCounter", string.Empty, TimeSpan.FromSeconds(10));
if (activity != null)
{
    Console.WriteLine("... activity is handled externally");
    Thread.Sleep(2000);
    Console.WriteLine("... activity handled successfully");
    await host.SubmitActivitySuccess(activity.Token, (int) activity.Parameters + 1);
}

await WaitForWorkflowInstanceToEnd(host, workflowInstanceId);


async Task WaitForWorkflowInstanceToEnd(IWorkflowHost host, string workflowInstanceId)
{
    var retryCount = 0;
    WorkflowInstance workflowInstance;
    do
    {
        workflowInstance = await host.PersistenceStore.GetWorkflowInstance(workflowInstanceId);
        retryCount++;
        Thread.Sleep(100);
    } while (workflowInstance.Status is WorkflowStatus.Runnable or WorkflowStatus.Suspended && retryCount<50);
}


Console.WriteLine("Workflow Host started");
Console.ReadKey();