using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Services;
internal class DataMapper : IDataMapper
{
    private IIdGenerator _idGenerator;

    public DataMapper(IIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public NewAlbumData MapAlbum(TrackFull track, int artistId)
    {
        return new NewAlbumData
        {
            SourceId = (int)track.Album.Source,
            ArtistId = artistId,
            Title = track.Album.Title,
            ReleaseTypeId = (int)track.Album.ReleaseType,
            Year = track.Album.Year,
            DiscCount = track.Album.DiscCount,
            Artwork = track.Album.ArtworkUrl,
            TagList = track.Album.Tags.ToString()
        };
    }

    public NewArtistData MapAlbumArtist(TrackFull track)
    {
        return new NewArtistData
        {
            NameId = _idGenerator.GenerateId(track.AlbumArtist.Name),
            Name = track.AlbumArtist.Name,
            GroupingId = (int)track.AlbumArtist.Grouping,
            Genre = track.AlbumArtist.Genre,
            City = track.AlbumArtist.City,
            State = track.AlbumArtist.State,
            Country = track.AlbumArtist.Country,
            Image = track.AlbumArtist.ImageUrl,
            TagList = track.AlbumArtist.Tags.ToString()
        };
    }

    public NewDiscData MapDisc(TrackFull track, int albumId)
    {
        var index = track.AlbumTrack.DiscNo;
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

    public NewTrackData MapTrack(TrackFull track, int artistId, int discId)
    {
        return new NewTrackData
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

    public NewArtistData MapTrackArtist(TrackFull track)
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
            Image = track.Artist.ImageUrl,
            TagList = track.Artist.Tags.ToString()
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
            ArtworkUrl = album.Artwork,
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
            ImageUrl = artist.Image,
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
