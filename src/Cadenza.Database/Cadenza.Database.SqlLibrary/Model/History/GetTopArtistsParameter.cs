namespace Cadenza.Database.SqlLibrary.Model.History;

internal class GetTopArtistsParameter
{
    public int HistoryPeriod { get; set; }
    public int MaxItems { get; set; }
}
