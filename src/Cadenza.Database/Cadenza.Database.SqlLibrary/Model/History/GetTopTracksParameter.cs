namespace Cadenza.Database.SqlLibrary.Model.History;

internal class GetTopTracksParameter
{
    public int HistoryPeriod { get; set; }
    public int MaxItems { get; set; }
}