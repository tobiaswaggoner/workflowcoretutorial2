using System.Collections.Immutable;

namespace WorkflowCoreTutorial.States;

public class ForEachState
{
    public List<string> Names { get; set; } = new () {"Name 1", "Name 2", "Name 3", "Name 4", "Name 5"};
}