﻿namespace Cadenza.API.SqlLibrary.Services;

internal class TrackAdder : ITrackAdder
{
    private readonly IDataMapper _mapper;
    private readonly IDataInsertService _insertService;

    public TrackAdder(IDataInsertService insertService, IDataMapper mapper)
    {
        _insertService = insertService;
        _mapper = mapper;
    }

    public async Task AddTrack(LibrarySource source, SyncTrackDTO track)
    {
        var trackArtistData = _mapper.MapTrackArtist(track);
        var trackArtistId = await _insertService.AddArtist(trackArtistData);

        var albumArtistId = trackArtistId;

        if (track.Artist.Name != track.Album.ArtistName)
        {
            var albumArtistData = _mapper.MapAlbumArtist(track);
            albumArtistId = await _insertService.AddArtist(albumArtistData);
        }

        var albumData = _mapper.MapAlbum(track, source, albumArtistId);
        var albumId = await _insertService.AddAlbum(albumData);

        var discData = _mapper.MapDisc(track, albumId);
        var discId = await _insertService.AddDisc(discData);

        var trackData = _mapper.MapTrack(track, trackArtistId, discId);
        await _insertService.AddTrack(trackData);
    }
}
