namespace Cadenza.Common;

public class SubTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<TaskStep> Steps { get; set; } = new List<TaskStep>();

    public void AddStep(string caption, Func<object, Task<object>> task)
    {
        Steps.Add(new TaskStep { Caption = caption, Task = task });
    }

    public void AddSteps<T>(string fetchCaption, string processCaption, Func<Task<T>> fetchTask, Func<T, Task> processTask) where T : class
    {
        AddStep(fetchCaption, async (r) =>
        {
            var result = await fetchTask();
            return result;
        });
        AddStep(processCaption, async (r) =>
        {
            await processTask(r as T);
            return true;
        });
    }
}
