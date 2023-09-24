namespace Cadenza.Web.Common.Tasks;

public class StartupTask
{
    public Connector Connector { get; set; }
    public string Title { get; set; }
    public TaskCheckStep CheckStep { get; set; }
    public List<TaskStep> Steps { get; set; } = new List<TaskStep>();
    public Action<Exception> OnError { get; set; }
    public Action OnCompleted { get; set; }

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
