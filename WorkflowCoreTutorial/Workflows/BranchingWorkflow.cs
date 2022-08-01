using WorkflowCore.Interface;
using WorkflowCoreTutorial.States;
using WorkflowCoreTutorial.Steps;

namespace WorkflowCoreTutorial.Workflows;

public class BranchingWorkflow : IWorkflow<BranchingState>
{
    public void Build(IWorkflowBuilder<BranchingState> builder)
    {
        var branchA = builder
            .CreateBranch()
            .StartWith<BranchAStep1>();
        
        var branchB = builder
            .CreateBranch()
            .StartWith<BranchBStep1>();

        var branchElse = builder
            .CreateBranch()
            .StartWith(ctx =>
            {
                Console.WriteLine("Branch should be either A or B");
            });

        builder
            .Decide(state => state.Branch)
            .Branch((state, _) => state.Branch == "A", branchA)
            .Branch((state, _) => state.Branch == "B", branchB)
            .Branch((state, _) => state.Branch != "A" && state.Branch != "B", branchElse);
    }

    public string Id => nameof(BranchingWorkflow);
    public int Version => 1;
}