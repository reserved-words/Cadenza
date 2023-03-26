using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model;
using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.SqlLibrary;
internal class MusicRepository : IMusicRepository
{
    private readonly IDataMapper _mapper;
    private readonly IInsertService _insertService;

    public MusicRepository(IInsertService insertService, IDataMapper mapper)
    {
        _insertService = insertService;
        _mapper = mapper;
    }

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        var trackArtistData = _mapper.MapTrackArtist(track);
        var trackArtistId = await _insertService.AddArtist(trackArtistData);

        var albumArtistId = trackArtistId;

        if (track.Artist.Name != track.Album.ArtistName)
        {
            var albumArtistData = _mapper.MapAlbumArtist(track);
            albumArtistId = await _insertService.AddArtist(albumArtistData);
        }

        var albumData = _mapper.MapAlbum(track, albumArtistId);
        var albumId = await _insertService.AddAlbum(albumData);

        var discData = _mapper.MapDisc(track, albumId);
        var discId = await _insertService.AddDisc(discData);

        var trackData = _mapper.MapTrack(track, trackArtistId, discId);
        await _insertService.AddTrack(trackData);
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
