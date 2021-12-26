namespace Cadenza.Common;

public enum TaskState
{
    None,
    
    Starting,
    Running,
    Completing,
    Cancelling,

    Cancelled,
    Completed,
    Errored
}
