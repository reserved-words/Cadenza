﻿using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class LibraryMapper : ILibraryMapper
{
    public AlbumDetailsDTO MapAlbum(GetAlbumsResult album, List<GetDiscsResult> discs)
    {
        return new AlbumDetailsDTO
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            Year = album.Year,
            DiscCount = album.DiscCount,
            DiscTrackCounts = discs.ToDictionary(d => d.Index, d => d.TrackCount),
            Tags = new TagsDTO(album.TagList)
        };
    }

    public AlbumTrackLinkDTO MapAlbumTrack(GetTracksResult track)
    {
        return new AlbumTrackLinkDTO
        {
            TrackId = track.Id,
            AlbumId = track.AlbumId,
            DiscNo = track.DiscIndex,
            TrackNo = track.TrackNo
        };
    }

    public ArtistDetailsDTO MapArtist(GetArtistsResult artist)
    {
        return new ArtistDetailsDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = new GroupingDTO(artist.GroupingId, artist.GroupingName),
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            Tags = new TagsDTO(artist.TagList)
        };
    }

    public TaggedItemDTO MapTaggedItem(GetTaggedItemsResult result)
    {
        var type = Enum.Parse< PlayerItemType>(result.Type);

        var albumDisplay = type == PlayerItemType.Track
            ? result.Album + (result.AlbumArtist == result.Artist ? "" : $" ({result.AlbumArtist})")
            : null;

        return new TaggedItemDTO
        { 
            Type = type,
            Id = result.Id,
            Name = result.Name,
            Artist = result.Artist,
            Album = result.Album,
            AlbumDisplay = albumDisplay
        };
    }

    public TrackDetailsDTO MapTrack(GetTracksResult track)
    {
        return new TrackDetailsDTO
        {
            Source = track.SourceId,
            Id = track.Id,
            IdFromSource = track.IdFromSource,
            ArtistId = track.ArtistId,
            ArtistName = track.ArtistName,
            AlbumId = track.AlbumId,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            Tags = new TagsDTO(track.TagList)
        };
    }
}
