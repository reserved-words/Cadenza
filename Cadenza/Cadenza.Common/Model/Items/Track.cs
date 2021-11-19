namespace Cadenza.Common;

public class Track
{
    public LibrarySource Source { get; set; }
    public string Id { get; set; }
    public string ArtistId { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }

    public TimeSpan DurationTimeSpan => TimeSpan.FromSeconds(DurationSeconds);
    public string Duration => DurationTimeSpan.ToString(DurationFormat);

    private string DurationFormat => DurationTimeSpan.Hours > 0
        ? @"hh\:mm\:ss"
        : @"mm\:ss";

    public override string ToString() => $"{ArtistName} - {Title}";
}