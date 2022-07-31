namespace WorkflowCoreTutorial.Services;

public class TimestampService
{
    public string TimeStamp => $"{DateTime.Now:HH:mm:ss}";
}