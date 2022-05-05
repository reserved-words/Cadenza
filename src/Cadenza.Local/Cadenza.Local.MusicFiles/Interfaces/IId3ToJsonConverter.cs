using Cadenza.Local.Common.Model.Json;
using Cadenza.Local.MusicFiles.Model;

namespace Cadenza.Local.MusicFiles.Interfaces;

internal interface IId3ToJsonConverter
{
    JsonArtist ConvertAlbumArtist(Id3Data id3Data);
    JsonAlbum ConvertAlbum(Id3Data id3Data);
    JsonTrack ConvertTrack(Id3Data id3Data);
    JsonArtist ConvertTrackArtist(Id3Data id3Data);
    JsonAlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data);
}