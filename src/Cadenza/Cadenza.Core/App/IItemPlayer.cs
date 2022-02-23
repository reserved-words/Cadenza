﻿namespace Cadenza.Core.App;

public interface IItemPlayer
{
    Task PlayArtist(string id);
    Task PlayAlbum(LibrarySource source, string id);
    Task PlayTrack(LibrarySource source, string id);
    Task PlayAll();
}
