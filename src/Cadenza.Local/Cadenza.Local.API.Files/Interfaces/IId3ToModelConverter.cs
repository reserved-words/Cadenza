using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3ToModelConverter
{
    ArtistInfo ConvertAlbumArtist(Id3Data id3Data);
    AlbumInfo ConvertAlbum(Id3Data id3Data);
    TrackInfo ConvertTrack(Id3Data id3Data);
    ArtistInfo ConvertTrackArtist(Id3Data id3Data);
    AlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data);
}