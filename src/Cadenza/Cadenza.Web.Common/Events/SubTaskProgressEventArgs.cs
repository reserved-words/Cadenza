namespace Cadenza.Web.Common.Events;

public delegate Task SubTaskProgressEventHandler(object sender, SubTaskProgressEventArgs e);

public class SubTaskProgressEventArgs : ProgressEventArgs
{
    public string Id { get; set; }
}
