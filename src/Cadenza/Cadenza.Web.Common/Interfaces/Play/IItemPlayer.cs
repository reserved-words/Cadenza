﻿namespace Cadenza.Web.Common.Interfaces.Play;

public interface IItemPlayer
{
    Task PlayAll();
    Task PlayGrouping(Grouping id);
    Task PlayGenre(string id);
    Task PlayArtist(string id);
    Task PlayAlbum(string id);
    Task PlayTrack(string id);
    Task PlayItem(PlayerItemType type, string id);
}
