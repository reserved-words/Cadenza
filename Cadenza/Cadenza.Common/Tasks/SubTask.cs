namespace Cadenza.Common;

public class SubTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<TaskStep> Steps { get; set; } = new List<TaskStep>();

    public void AddStep(string caption, Func<Task> task)
    {
        Steps.Add(new TaskStep { Caption = caption, Task = task });
    }
}
