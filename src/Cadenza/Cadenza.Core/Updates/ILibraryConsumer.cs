﻿namespace Cadenza.Core.Updates;

public interface ILibraryConsumer
{
    //event AlbumUpdatedEventHandler AlbumUpdated;
    event ArtistUpdatedEventHandler ArtistUpdated;
    //event TrackUpdatedEventHandler TrackUpdated;

}