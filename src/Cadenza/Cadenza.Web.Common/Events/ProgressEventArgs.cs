namespace Cadenza.Web.Common.Events;

public class ProgressEventArgs : EventArgs
{
    public string Message { get; set; }
    public TaskState State { get; set; }
}