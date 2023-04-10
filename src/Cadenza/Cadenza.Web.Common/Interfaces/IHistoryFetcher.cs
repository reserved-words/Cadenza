﻿namespace Cadenza.Web.Common.Interfaces;

public interface IHistoryFetcher
{
    Task<List<RecentAlbum>> GetRecentAlbums(int maxItems);
}