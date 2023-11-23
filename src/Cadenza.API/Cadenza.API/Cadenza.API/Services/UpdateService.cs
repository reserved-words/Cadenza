﻿using Cadenza.API.Interfaces.Services;
using Cadenza.Common.DTO.Attributes;

namespace Cadenza.API.Services;

internal class UpdateService : IUpdateService
{
    private readonly IMusicRepository _musicRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly ICachePopulater _cachePopulater;

    public UpdateService(IQueueRepository queueRepository, IMusicRepository musicRepository, ICachePopulater cachePopulater)
    {
        _queueRepository = queueRepository;
        _musicRepository = musicRepository;
        _cachePopulater = cachePopulater;
    }

    public async Task UpdateAlbumTracks(UpdateAlbumTracksDTO request)
    {
        foreach (var originalTrack in request.OriginalTracks)
        {
            var updatedTrack = request.UpdatedTracks
                .SingleOrDefault(t => t.TrackId == originalTrack.TrackId);

            if (updatedTrack == null)
            {
                await _queueRepository.AddRemovalRequest(originalTrack.TrackId);
                await _musicRepository.RemoveTrack(originalTrack.TrackId);
            }
            else
            {
                var trackUpdateRequest = new ItemUpdateRequestDTO
                {
                    Id = originalTrack.TrackId,
                    Type = LibraryItemType.Track,
                    Updates = GetUpdates(originalTrack, updatedTrack)
                };

                if (!trackUpdateRequest.Updates.Any())
                    continue;

                await _queueRepository.AddUpdateRequest(trackUpdateRequest);
                await _musicRepository.UpdateTrack(trackUpdateRequest);
            }
        }

        await _cachePopulater.Populate(false);
    }

    public async Task UpdateTrack(UpdateTrackDTO dto)
    {
        var request = new ItemUpdateRequestDTO
        {
            Id = dto.OriginalTrack.Id,
            Type = LibraryItemType.Track,
            Updates = GetUpdates(dto.OriginalTrack, dto.UpdatedTrack)
        };

        if (!request.Updates.Any())
            return;

        await _queueRepository.AddUpdateRequest(request);
        await _musicRepository.UpdateTrack(request);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateAlbum(UpdateAlbumDTO dto)
    {
        var request = new ItemUpdateRequestDTO
        {
            Id = dto.OriginalAlbum.Id,
            Type = LibraryItemType.Album,
            Updates = GetUpdates(dto.OriginalAlbum, dto.UpdatedAlbum)
        };

        if (!request.Updates.Any())
            return;

        await _queueRepository.AddUpdateRequest(request);
        await _musicRepository.UpdateAlbum(request);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateArtist(UpdateArtistDTO dto)
    {
        var request = new ItemUpdateRequestDTO
        {
            Id = dto.OriginalArtist.Id,
            Type = LibraryItemType.Artist,
            Updates = GetUpdates(dto.OriginalArtist, dto.UpdatedArtist)
        };

        if (!request.Updates.Any())
            return;

        await _queueRepository.AddUpdateRequest(request);
        await _musicRepository.UpdateArtist(request);
        await _cachePopulater.Populate(false);
    }

    private List<PropertyUpdateDTO> GetUpdates<T>(T originalItem, T updatedItem)
    {
        var updates = new List<PropertyUpdateDTO>();

        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var itemProperty = property.GetCustomAttributes(false)
                .OfType<ItemPropertyAttribute>()
                .SingleOrDefault();

            if (itemProperty == null)
                continue;

            var originalValue = property.GetValue(originalItem)?.ToString();
            var updatedValue = property.GetValue(updatedItem)?.ToString();

            if (AreEqual(originalValue, updatedValue))
                continue;

            updates.Add(new PropertyUpdateDTO
            {
                Property = itemProperty.Property,
                OriginalValue = originalValue,
                UpdatedValue = updatedValue
            });
        }

        return updates;
    }

    private static bool AreEqual(string originalValue, string updatedValue)
    {
        if (originalValue == null && updatedValue == null)
            return true;

        if (originalValue == null || updatedValue == null)
            return false;

        return originalValue == updatedValue;
    }
}