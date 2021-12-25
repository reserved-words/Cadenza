﻿namespace Cadenza.Player;

public interface ILibraryConsumer
{
    event AlbumUpdatedEventHandler AlbumUpdated;
    event ArtistUpdatedEventHandler ArtistUpdated;
    event TrackUpdatedEventHandler TrackUpdated;

}