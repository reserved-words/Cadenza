namespace Cadenza.Web.Common.Tasks;

public enum TaskState
{
    None,

    Running,
    Completed,
    Errored,
    [Display(Name = "Completed with errors")]
    CompletedWithErrors
}
