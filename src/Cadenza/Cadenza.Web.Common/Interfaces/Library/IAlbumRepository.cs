using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces.Library;

public interface IAlbumRepository
{
    Task<AlbumDetails> GetAlbum(int id);
    Task<List<AlbumTrack>> GetAlbumTracks(int albumId);
}
