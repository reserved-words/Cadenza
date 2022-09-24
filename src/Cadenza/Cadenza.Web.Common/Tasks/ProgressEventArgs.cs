namespace Cadenza.Web.Common.Tasks;

public delegate Task TaskGroupProgressEventHandler(object sender, TaskGroupProgressEventArgs e);
public delegate Task SubTaskProgressEventHandler(object sender, SubTaskProgressEventArgs e);

public class TaskGroupProgressEventArgs : ProgressEventArgs
{
}

public class SubTaskProgressEventArgs : ProgressEventArgs
{
    public string Id { get; set; }
}

public class ProgressEventArgs : EventArgs
{
    public string Message { get; set; }
    public TaskState State { get; set; }
}