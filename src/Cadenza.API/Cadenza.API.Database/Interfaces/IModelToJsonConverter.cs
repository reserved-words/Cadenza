namespace Cadenza.API.Database.Interfaces;

internal interface IModelToJsonConverter
{
    JsonArtist ConvertArtist(ArtistInfo artist);
    JsonAlbum ConvertAlbum(AlbumInfo album);
    JsonTrack ConvertTrack(TrackInfo track);
    JsonAlbumTrack ConvertAlbumTrackLink(AlbumTrackLink albumTrackLink);
}