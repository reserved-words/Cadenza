using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IUpdateMapper
{
    UpdateAlbumParameter MapAlbumToUpdate(int id, GetAlbumForUpdateResult album);
    UpdateArtistParameter MapArtistToUpdate(int id, GetArtistForUpdateResult artist);
    UpdateTrackParameter MapTrackToUpdate(int id, GetTrackForUpdateResult track);

    AddArtistParameter MapTrackArtist(SyncTrackDTO track);
    AddArtistParameter MapAlbumArtist(SyncTrackDTO track);
    AddAlbumParameter MapAlbum(SyncTrackDTO track, LibrarySource source, int artistId);
    AddDiscParameter MapDisc(SyncTrackDTO track, int albumId);
    AddTrackParameter MapTrack(SyncTrackDTO track, int artistId, int discId);

}