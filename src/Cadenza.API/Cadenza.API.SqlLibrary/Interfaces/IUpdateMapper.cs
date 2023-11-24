using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IUpdateMapper
{
    AddArtistParameter MapTrackArtist(SyncTrackDTO track);
    AddArtistParameter MapAlbumArtist(SyncTrackDTO track);
    AddAlbumParameter MapAlbum(SyncTrackDTO track, LibrarySource source, int artistId);
    AddDiscParameter MapDisc(SyncTrackDTO track, int albumId);
    AddTrackParameter MapTrack(SyncTrackDTO track, int artistId, int discId);

}