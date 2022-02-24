using System.ComponentModel.DataAnnotations;

namespace Cadenza.Core.Tasks;

public enum TaskState
{
    None,

    Starting,
    Running,
    Completing,
    Cancelling,

    Cancelled,
    Completed,
    Errored,
    [Display(Name = "Completed with errors")]
    CompletedWithErrors
}
