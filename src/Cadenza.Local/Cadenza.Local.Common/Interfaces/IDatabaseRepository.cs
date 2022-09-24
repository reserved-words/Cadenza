using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.Local.Common.Interfaces;

public interface IDatabaseRepository
{
    Task<DateTime> GetDateLastUpdated();
    Task<DateTime> SetDateLastUpdated(DateTime dateTime);
    Task AddOrUpdateArtist(ArtistInfo artist);
    Task AddOrUpdateAlbum(AlbumInfo album);
    Task AddOrUpdateTrack(TrackInfo trackInfo, AlbumTrackLink albumTrackLink);
    Task<List<string>> GetAllTracks();
    Task RemoveTrack(string id);
    Task<List<ItemPropertyUpdate>> GetUpdates();
    Task MarkUpdated(LibraryItemType itemType, string id);
}