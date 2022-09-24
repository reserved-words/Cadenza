namespace Cadenza.Web.Common.Tasks;

public class TaskGroup
{
    public string Title { get; set; }
    public List<SubTask> Tasks { get; set; } = new List<SubTask>();
    public Func<Task> PreTask { get; set; }
}
