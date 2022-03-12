using System.ComponentModel.DataAnnotations;

namespace Cadenza.Domain;

public enum HistoryPeriod
{
    [Display(Name = "All time")]
    Overall,
    [Display(Name = "Last week")]
    Week,
    [Display(Name = "Last month")]
    Month,
    [Display(Name = "Last 3 months")]
    QuarterYear,
    [Display(Name = "Last 6 months")]
    HalfYear,
    [Display(Name = "Last year")]
    Year
}
