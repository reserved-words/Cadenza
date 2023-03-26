using Cadenza.API.Interfaces.Repositories;
using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model;
using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.SqlLibrary;
internal class MusicRepository : IMusicRepository
{
    public Task AddTrack(LibrarySource source, TrackFull track)
    {
        throw new NotImplementedException();
    }

    public Task<FullLibrary> Get(LibrarySource? source)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTracks(LibrarySource source, List<string> id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArtist(ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        throw new NotImplementedException();
    }
}
