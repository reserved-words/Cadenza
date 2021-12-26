namespace Cadenza.Common;

public class TaskStep
{
    public string Caption { get; set; }
    public Func<Task> Task { get; set; }
}
