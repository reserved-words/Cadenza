using System.ComponentModel.DataAnnotations;

namespace Cadenza.Web.Common.Tasks;

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
