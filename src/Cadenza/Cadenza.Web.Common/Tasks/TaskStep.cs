namespace Cadenza.Web.Common.Tasks;

public class TaskStep
{
    public string Caption { get; set; }
    public Func<object, CancellationToken, Task<object>> Task { get; set; }
}
