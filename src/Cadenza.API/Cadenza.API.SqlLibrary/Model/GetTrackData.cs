namespace Cadenza.API.SqlLibrary.Model;

internal class GetTrackData : TrackDataBase
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public int DiscIndex { get; set; }
    public LibrarySource SourceId { get; internal set; }
    public string ArtistNameId { get; internal set; }
    public string ArtistName { get; internal set; }
}
