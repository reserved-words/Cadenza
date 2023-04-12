﻿namespace Cadenza.Web.Common.Interfaces.Play;

public interface IItemPlayer
{
    Task PlayAll();
    Task PlayAlbum(string id, string startTrackId = null);
    Task PlayArtist(string id);
    Task PlayGenre(string id);
    Task PlayGrouping(Grouping id);
    Task PlayTag(string id);
    Task PlayTrack(string id);
}
