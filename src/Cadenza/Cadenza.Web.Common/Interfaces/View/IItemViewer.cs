﻿namespace Cadenza.Web.Common.Interfaces.View;

public interface IItemViewer
{
    Task ViewAlbum(int id, string title);
    Task ViewArtist(string id, string name);
    Task ViewArtist(string name);
    Task ViewGenre(string id);
    Task ViewGrouping(Grouping id);
    Task ViewPlaying(PlaylistId playlist);
    Task ViewSearchResult(PlayerItem item);
    Task ViewTag(string id);
    Task ViewTrack(string id, string title);
}