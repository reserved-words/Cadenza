namespace Cadenza.API.Database.Interfaces;

internal interface IJsonToModelConverter
{
    ArtistInfo ConvertArtist(JsonArtist artist);
    AlbumInfo ConvertAlbum(JsonAlbum album, ICollection<JsonArtist> artists);
    TrackInfo ConvertTrack(JsonTrack track, ICollection<JsonArtist> artists);
    AlbumTrackLink ConvertAlbumTrack(JsonAlbumTrack albumTrackLink);
    FullLibrary Convert(JsonItems library);
}