using System.ComponentModel.DataAnnotations;

namespace Cadenza.Common.Enums;

public enum HistoryPeriod
{
    [Display(Name = "All time")]
    Overall = 0,
    [Display(Name = "Last week")]
    Week = 1,
    [Display(Name = "Last month")]
    Month = 2,
    [Display(Name = "Last 3 months")]
    QuarterYear = 3,
    [Display(Name = "Last 6 months")]
    HalfYear = 4,
    [Display(Name = "Last year")]
    Year = 5
}
