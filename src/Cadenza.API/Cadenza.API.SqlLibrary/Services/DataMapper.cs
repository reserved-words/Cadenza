using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Interfaces.Utilities;

namespace Cadenza.API.SqlLibrary.Services;
internal class DataMapper : IDataMapper
{
    private IIdGenerator _idGenerator;

    public DataMapper(IIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public AddAlbumData MapAlbum(TrackFull track, int artistId)
    {
        return new AddAlbumData
        {
            SourceId = (int)track.Album.Source,
            ArtistId = artistId,
            Title = track.Album.Title,
            ReleaseTypeId = (int)track.Album.ReleaseType,
            Year = track.Album.Year,
            ArtworkUrl = track.Album.ArtworkUrl,
            TagList = track.Album.Tags.ToString()
        };
    }

    public AddArtistData MapAlbumArtist(TrackFull track)
    {
        return new AddArtistData
        {
            NameId = _idGenerator.GenerateId(track.AlbumArtist.Name),
            Name = track.AlbumArtist.Name,
            GroupingId = (int)track.AlbumArtist.Grouping,
            Genre = track.AlbumArtist.Genre,
            City = track.AlbumArtist.City,
            State = track.AlbumArtist.State,
            Country = track.AlbumArtist.Country,
            ImageUrl = track.AlbumArtist.ImageUrl,
            TagList = track.AlbumArtist.Tags.ToString()
        };
    }

    public AddDiscData MapDisc(TrackFull track, int albumId)
    {
        var index = track.AlbumTrack.DiscNo;
        if (index <= 0)
            index = 1;

        var trackCount = track.Album.TrackCounts.Count >= index
            ? track.Album.TrackCounts[index - 1]
            : 0;

        return new AddDiscData
        {
            AlbumId = albumId,
            Index = index,
            TrackCount = trackCount
        };
    }

    public AddTrackData MapTrack(TrackFull track, int artistId, int discId)
    {
        return new AddTrackData
        {
            IdFromSource = track.Track.Id,
            ArtistId = artistId,
            DiscId = discId,
            TrackNo = track.AlbumTrack.TrackNo,
            Title = track.Track.Title,
            DurationSeconds = track.Track.DurationSeconds,
            Year = track.Track.Year,
            Lyrics = track.Track.Lyrics,
            TagList = track.Track.Tags.ToString()
        };
    }

    public AddArtistData MapTrackArtist(TrackFull track)
    {
        return new AddArtistData
        {
            NameId = _idGenerator.GenerateId(track.Artist.Name),
            Name = track.Artist.Name,
            GroupingId = (int)track.Artist.Grouping,
            Genre = track.Artist.Genre,
            City = track.Artist.City,
            State = track.Artist.State,
            Country = track.Artist.Country,
            ImageUrl = track.Artist.ImageUrl,
            TagList = track.Artist.Tags.ToString()
        };
    }
}
