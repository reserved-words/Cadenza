namespace Cadenza.Web.Common.Tasks;

public class TaskCheckStep
{
    public string Caption { get; set; }
    public Func<Task<bool>> Task { get; set; }
}