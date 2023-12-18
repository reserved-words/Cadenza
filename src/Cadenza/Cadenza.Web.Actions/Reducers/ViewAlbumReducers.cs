﻿using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Reducers;

public static class ViewAlbumReducers
{
    [ReducerMethod(typeof(FetchViewAlbumRequest))]
    public static ViewAlbumState ReduceFetchViewAlbumAction(ViewAlbumState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewAlbumState ReduceFetchViewAlbumResult(ViewAlbumState state, FetchViewAlbumResult action) => state with
    {
        IsLoading = false,
        Album = action.Album
    };

    [ReducerMethod]
    public static ViewAlbumState ReduceAlbumTracksUpdatedAction(ViewAlbumState state, AlbumTracksUpdatedAction action)
    {
        if (state.Album.Album.Id != action.AlbumId)
            return state;

        var tracks = new List<AlbumTrackVM>();

        foreach (var disc in state.Album.Discs)
        {
            foreach (var track in disc.Tracks)
            {
                if (action.RemovedTracks.Contains(track.TrackId))
                    continue;

                var updatedTrack = action.UpdatedAlbumTracks.SingleOrDefault(t => t.TrackId == track.TrackId) ?? track;
                tracks.Add(updatedTrack);
            }
        }

        return state with
        {
            Album = state.Album with
            {
                Discs = tracks.GroupByDisc()
            }
        };
    }

    [ReducerMethod]
    public static ViewAlbumState ReduceAlbumUpdatedAction(ViewAlbumState state, AlbumUpdatedAction action)
    {
        if (state.Album == null || state.Album.Album.Id != action.UpdatedAlbum.Id)
            return state;

        var updatedAlbum = state.Album.Album with
        {
            Title = action.UpdatedAlbum.Title,
            ReleaseType = action.UpdatedAlbum.ReleaseType,
            Year = action.UpdatedAlbum.Year,
            DiscCount = action.UpdatedAlbum.DiscCount,
            ArtworkBase64 = action.UpdatedAlbum.ArtworkBase64,
            Tags = action.UpdatedAlbum.Tags
        };

        return state with 
        { 
            Album = state.Album with
            {
                Album = updatedAlbum
            }
        };
    }
}
