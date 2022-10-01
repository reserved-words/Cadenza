namespace Cadenza.Web.Common.Events;

public delegate Task TaskGroupProgressEventHandler(object sender, TaskGroupProgressEventArgs e);

public class TaskGroupProgressEventArgs : ProgressEventArgs
{
}
