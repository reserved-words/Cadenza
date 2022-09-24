using Cadenza.Local.Common.Model;
using Cadenza.Local.MusicFiles.Model;

namespace Cadenza.Local.MusicFiles.Interfaces;

internal interface IId3ToLocalConverter
{
    LocalArtist ConvertAlbumArtist(Id3Data id3Data);
    LocalAlbum ConvertAlbum(Id3Data id3Data);
    LocalTrack ConvertTrack(Id3Data id3Data);
    LocalArtist ConvertTrackArtist(Id3Data id3Data);
    LocalAlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data);
}