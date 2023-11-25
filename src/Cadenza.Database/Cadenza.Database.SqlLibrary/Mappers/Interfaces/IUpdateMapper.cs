using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IUpdateMapper
{
    UpdateAlbumParameter MapAlbumToUpdate(GetAlbumForUpdateResult album);
    UpdateArtistParameter MapArtistToUpdate(GetArtistForUpdateResult artist);
    UpdateTrackParameter MapTrackToUpdate(GetTrackForUpdateResult track);

    AddArtistParameter MapTrackArtist(SyncTrackDTO track);
    AddArtistParameter MapAlbumArtist(SyncTrackDTO track);
    AddAlbumParameter MapAlbum(SyncTrackDTO track, LibrarySource source, int artistId);
    AddDiscParameter MapDisc(SyncTrackDTO track, int albumId);
    AddTrackParameter MapTrack(SyncTrackDTO track, int artistId, int discId);

}