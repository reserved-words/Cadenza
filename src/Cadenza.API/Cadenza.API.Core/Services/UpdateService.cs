using Cadenza.Common.DTO.Attributes;
using Cadenza.Common.Enums;

namespace Cadenza.API.Core.Services;

internal class UpdateService : IUpdateService
{
    private readonly IMusicRepository _musicRepository;
    private readonly IUpdateRepository _updateRepository;
    private readonly ICachePopulater _cachePopulater;

    public UpdateService(IUpdateRepository updateRepository, IMusicRepository musicRepository, ICachePopulater cachePopulater)
    {
        _updateRepository = updateRepository;
        _musicRepository = musicRepository;
        _cachePopulater = cachePopulater;
    }

    public async Task RemoveTrack(TrackRemovalRequestDTO request)
    {
        await _updateRepository.AddRemovalRequest(request);
        await _musicRepository.RemoveTrack(request.TrackId);
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

        await _updateRepository.AddUpdateRequest(request);
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

        await _updateRepository.AddUpdateRequest(request);
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

        await _updateRepository.AddUpdateRequest(request);
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