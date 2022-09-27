using Cadenza.Domain.Model.Album;
using Cadenza.Domain.Model.Artist;
using Cadenza.Domain.Model.Track;
using Cadenza.Local.API.Files.Model;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3ToModelConverter
{
    ArtistInfo ConvertAlbumArtist(Id3Data id3Data);
    AlbumInfo ConvertAlbum(Id3Data id3Data);
    TrackInfo ConvertTrack(Id3Data id3Data);
    ArtistInfo ConvertTrackArtist(Id3Data id3Data);
    AlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data);
}