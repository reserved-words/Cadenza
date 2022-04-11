using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces.Converters;

public interface IId3ToJsonConverter
{
    JsonArtist ConvertAlbumArtist(Id3Data id3Data);
    JsonAlbum ConvertAlbum(Id3Data id3Data);
    JsonTrack ConvertTrack(Id3Data id3Data);
    JsonArtist ConvertTrackArtist(Id3Data id3Data);
    JsonAlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data);
}