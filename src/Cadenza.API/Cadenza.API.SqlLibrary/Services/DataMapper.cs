﻿using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Services;
internal class DataMapper : IDataMapper
{
    private IIdGenerator _idGenerator;

    public DataMapper(IIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public NewAlbumData MapAlbum(SyncTrack track, int artistId)
    {
        return new NewAlbumData
        {
            SourceId = (int)track.Source,
            ArtistId = artistId,
            Title = track.Album.Title,
            ReleaseTypeId = (int)track.Album.ReleaseType,
            Year = track.Album.Year,
            DiscCount = track.Album.TrackCounts.Count,
            TagList = track.Album.TagList,
            ArtworkMimeType = track.Album.ArtworkMimeType,
            ArtworkContent = track.Album.ArtworkContent
        };
    }

    public NewArtistData MapAlbumArtist(SyncTrack track)
    {
        return new NewArtistData
        {
            NameId = _idGenerator.GenerateId(track.Album.ArtistName),
            Name = track.Album.ArtistName,
            GroupingId = (int)Grouping.None,
            Genre = "None"
        };
    }

    public NewDiscData MapDisc(SyncTrack track, int albumId)
    {
        var index = track.DiscNo;
        if (index <= 0)
            index = 1;

        var trackCount = track.Album.TrackCounts.Count >= index
            ? track.Album.TrackCounts[index - 1]
            : 0;

        return new NewDiscData
        {
            AlbumId = albumId,
            Index = index,
            TrackCount = trackCount
        };
    }

    public NewTrackData MapTrack(SyncTrack track, int artistId, int discId)
    {
        return new NewTrackData
        {
            IdFromSource = track.Id,
            ArtistId = artistId,
            DiscId = discId,
            TrackNo = track.TrackNo,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            TagList = track.TagList
        };
    }

    public NewArtistData MapTrackArtist(SyncTrack track)
    {
        return new NewArtistData
        {
            NameId = _idGenerator.GenerateId(track.Artist.Name),
            Name = track.Artist.Name,
            GroupingId = (int)track.Artist.Grouping,
            Genre = track.Artist.Genre,
            City = track.Artist.City,
            State = track.Artist.State,
            Country = track.Artist.Country,
            TagList = track.Artist.TagList,
            ImageMimeType = track.Artist.ImageMimeType,
            ImageContent = track.Artist.ImageContent
        };
    }



    public AlbumInfo MapAlbum(GetAlbumData album, List<GetDiscData> discs)
    {
        return new AlbumInfo
        {
            Source = (LibrarySource)album.SourceId,
            Id = album.Id.ToString(),
            ArtistId = album.ArtistNameId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            Year = album.Year,
            DiscCount = album.DiscCount,
            TrackCounts = discs.Select(d => d.TrackCount).ToList(),
            Tags = new TagList(album.TagList)
        };
    }

    public AlbumTrackLink MapAlbumTrack(GetTrackData track)
    {
        return new AlbumTrackLink
        {
            TrackId = track.IdFromSource,
            AlbumId = track.AlbumId.ToString(),
            DiscNo = track.DiscIndex,
            TrackNo = track.TrackNo
        };
    }

    public ArtistInfo MapArtist(GetArtistData artist)
    {
        return new ArtistInfo
        {
            Id = artist.NameId,
            Name = artist.Name,
            Grouping = (Grouping)artist.GroupingId,
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            Tags = new TagList(artist.TagList)
        };
    }

    public TrackInfo MapTrack(GetTrackData track)
    {
        return new TrackInfo
        {
            Source = (LibrarySource)track.SourceId,
            Id = track.IdFromSource,
            ArtistId = track.ArtistNameId,
            ArtistName = track.ArtistName,
            AlbumId = track.AlbumId.ToString(),
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            Tags = new TagList(track.TagList)
        };
    }
}