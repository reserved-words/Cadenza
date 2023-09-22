namespace Cadenza.Web.Common.Tasks;

public class SubTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public TaskCheckStep CheckStep { get; set; }
    public List<TaskStep> Steps { get; set; } = new List<TaskStep>();
    public Action<Exception> OnError { get; set; }
    public Action OnCompleted { get; set; }

    public void AddStep(string caption, Func<object, CancellationToken, Task<object>> task)
    {
        Steps.Add(new TaskStep { Caption = caption, Task = task });
    }

    public void AddStep(string caption, Func<Task> task)
    {
        Steps.Add(new TaskStep
        {
            Caption = caption,
            Task = new Func<object, CancellationToken, Task<object>>(async (o, ct) =>
            {
                await task();
                return true;
            })
        });
    }

    public void AddSteps(
        string firstStep,
        string lastStep,
        Func<CancellationToken, Task<string>> firstTask,
        Func<string, CancellationToken, Task> lastTask,
        params (string Caption, Func<string, CancellationToken, Task<string>> Task)[] intermediateSteps)
    {
        AddStep(firstStep, async (o1, ct) =>
        {
            return await firstTask(ct);
        });

        foreach (var step in intermediateSteps)
        {
            AddStep(step.Caption, async (o, ct) =>
            {
                return await step.Task((string)o, ct);
            });
        }

        AddStep(lastStep, async (o3, ct) =>
        {
            await lastTask((string)o3, ct);
            return null;
        });
    }
}
