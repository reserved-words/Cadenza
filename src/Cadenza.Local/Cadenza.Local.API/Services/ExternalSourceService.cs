using Cadenza.Local.API.Interfaces;

namespace Cadenza.Local.API;

public class ExternalSourceService : IExternalSourceService
{
    public Task AddAlbum(AlbumInfo album)
    {
        throw new NotImplementedException();
    }

    public Task AddArtist(Artist artist)
    {
        throw new NotImplementedException();
    }

    public Task AddTrack(TrackInfo track, AlbumTrackPosition position)
    {
        throw new NotImplementedException();
    }
}
