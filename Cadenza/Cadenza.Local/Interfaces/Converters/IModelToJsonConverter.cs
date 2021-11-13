namespace Cadenza.Local;

public interface IModelToJsonConverter
{
    JsonArtist ConvertArtist(ArtistInfo artist);
    JsonAlbum ConvertAlbum(AlbumInfo album);
    JsonTrack ConvertTrack(TrackInfo track);
    JsonAlbumTrackLink ConvertAlbumTrackLink(AlbumTrackLink albumTrackLink);
}
