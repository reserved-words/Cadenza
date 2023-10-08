using Cadenza.Common.Enums;

namespace Cadenza.API.SqlLibrary.Model;

internal class GetTrackData : TrackDataBase
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public int DiscIndex { get; set; }
    public LibrarySource SourceId { get; set; }
    public string ArtistName { get; set; }
}
