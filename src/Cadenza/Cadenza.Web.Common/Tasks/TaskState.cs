namespace Cadenza.Web.Common.Tasks;

public enum TaskState
{
    None,

    Starting,
    Running,
    Completing,

    Completed,
    Errored,
    [Display(Name = "Completed with errors")]
    CompletedWithErrors
}
