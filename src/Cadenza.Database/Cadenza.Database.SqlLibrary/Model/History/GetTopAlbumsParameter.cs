namespace Cadenza.Database.SqlLibrary.Model.History;

internal class GetTopAlbumsParameter
{
    public int HistoryPeriod { get; set; }
    public int MaxItems { get; set; }
}
