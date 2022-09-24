using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;
using Cadenza.Local.MusicFiles.Model;

namespace Cadenza.Local.MusicFiles.Interfaces;

internal interface IId3ToModelConverter
{
    ArtistInfo ConvertAlbumArtist(Id3Data id3Data);
    AlbumInfo ConvertAlbum(Id3Data id3Data);
    TrackInfo ConvertTrack(Id3Data id3Data);
    ArtistInfo ConvertTrackArtist(Id3Data id3Data);
    AlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data);
}