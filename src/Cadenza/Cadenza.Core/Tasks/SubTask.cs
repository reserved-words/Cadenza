namespace Cadenza.Common;

public class SubTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public TaskCheckStep CheckStep { get; set; }
    public List<TaskStep> Steps { get; set; } = new List<TaskStep>();
    public Func<Exception, Task> OnError { get; set; }
    public Func<Task> OnCompleted { get; set; }

    public void AddStep(string caption, Func<object, Task<object>> task)
    {
        Steps.Add(new TaskStep { Caption = caption, Task = task });
    }

    public void AddStep(string caption, Func<Task> task)
    {
        Steps.Add(new TaskStep
        {
            Caption = caption,
            Task = new Func<object, Task<object>>(async (o) =>
            {
                await task();
                return true;
            })
        });
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

    public void AddSteps(
        string firstStep,
        string lastStep,
        Func<Task<string>> firstTask,
        Func<string, Task> lastTask,
        params (string Caption, Func<string, Task<string>> Task)[] intermediateSteps)
    {
        AddStep(firstStep, async (o1) =>
        {
            return await firstTask();
        });

        foreach (var step in intermediateSteps)
        {
            AddStep(step.Caption, async (o) =>
            {
                return await step.Task((string)o);
            });
        }

        AddStep(lastStep, async (o3) =>
        {
            await lastTask((string)o3);
            return null;
        });
    }
}
