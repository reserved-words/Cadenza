namespace Cadenza.Player;

public interface IAlbumUpdater
{
    Task UpdateAlbum(AlbumUpdate album);
}