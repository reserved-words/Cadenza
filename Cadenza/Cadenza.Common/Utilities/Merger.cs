namespace Cadenza.Common;

public class Merger : IMerger
{
    private readonly IValueMerger _merger;

    public Merger(IValueMerger merger)
    {
        _merger = merger;
    }

    public void MergeArtist(Artist artist, Artist update, bool forceUpdate)
    {
        artist.Id = Merge(artist.Id, update.Id, forceUpdate);
        artist.Name = Merge(artist.Name, update.Name, forceUpdate);
        artist.Grouping = Merge(artist.Grouping, update.Grouping, forceUpdate);
        artist.Genre = Merge(artist.Genre, update.Genre, forceUpdate);

        foreach (var sourceId in update.SourceIds)
        {
            artist.AddSourceId(sourceId);
        }
    }

    public void MergeArtistInfo(ArtistInfo artist, ArtistInfo update, bool forceUpdate)
    {
        MergeArtist(artist, update, forceUpdate);

        artist.City = Merge(artist.City, update.City, forceUpdate);
        artist.State = Merge(artist.State, update.State, forceUpdate);
        artist.Country = Merge(artist.Country, update.Country, forceUpdate);
        artist.Links = MergeCollection(artist.Links, update.Links, forceUpdate);
    }

    public void MergeAlbum(AlbumInfo album, AlbumInfo update, bool forceUpdate)
    {
        album.Id = Merge(album.Id, update.Id, forceUpdate);
        album.ArtistId = Merge(album.ArtistId, update.ArtistId, forceUpdate);
        album.ArtistName = Merge(album.ArtistName, update.ArtistName, forceUpdate);
        album.Title = Merge(album.Title, update.Title, forceUpdate);
        album.Year = Merge(album.Year, update.Year, forceUpdate);
        album.ImageUrl = Merge(album.ImageUrl, update.ImageUrl, forceUpdate);
        album.DiscCount = Merge(album.DiscCount, update.DiscCount, forceUpdate);
        album.ReleaseType = Merge(album.ReleaseType, update.ReleaseType, forceUpdate);
        album.Source = Merge(album.Source, update.Source, forceUpdate);
        album.TrackCounts = MergeTrackCounts(album.TrackCounts, update.TrackCounts, forceUpdate);
    }

    public void MergeTrack(TrackInfo track, TrackInfo update, bool forceUpdate)
    {
        track.Id = Merge(track.Id, update.Id, forceUpdate);
        track.Title = Merge(track.Title, update.Title, forceUpdate);
        track.ArtistId = Merge(track.ArtistId, update.ArtistId, forceUpdate);
        track.ArtistName = Merge(track.ArtistName, update.ArtistName, forceUpdate);
        track.Lyrics = Merge(track.Lyrics, update.Lyrics, forceUpdate);
        track.Year = Merge(track.Year, update.Year, forceUpdate);
        track.Tags = MergeCollection(track.Tags, update.Tags, forceUpdate);
        track.DurationSeconds = Merge(track.DurationSeconds, update.DurationSeconds, forceUpdate);
        track.Source = Merge(track.Source, update.Source, forceUpdate);
    }

    public void MergeAlbumArtist(AlbumLinks albumLinks, string artistId)
    {
        albumLinks.ArtistId = artistId;
    }

    public void MergeAlbumTrack(AlbumLinks albumLinks, string trackId, AlbumTrackPosition position)
    {
        var link = albumLinks.Tracks.GetOrAdd(lnk => lnk.TrackId == trackId);

        link.AlbumId ??= albumLinks.AlbumId;
        link.TrackId ??= trackId;

        if (link.Position.TrackNo == 0)
        {
            link.Position = position;
        }
    }

    public void MergeArtistAlbum(ArtistLinks artistLinks, string albumId)
    {
        artistLinks.Albums.AddIfNotPresent(albumId);
    }

    public void MergeArtistTrack(ArtistLinks artistLinks, string trackId)
    {
        artistLinks.Tracks.AddIfNotPresent(trackId);
    }

    public void MergeTrackArtist(TrackLinks trackLinks, string artistId)
    {
        trackLinks.ArtistId = artistId;
    }

    public void MergeTrackAlbum(TrackLinks trackLinks, string albumId, AlbumTrackPosition position)
    {
        trackLinks.AlbumId ??= albumId;

        if (trackLinks.Position.TrackNo == 0)
        {
            trackLinks.Position = position;
        }
    }

    private int Merge(int original, int update, bool forceUpdate)
    {
        return _merger.Merge(original, update, forceUpdate);
    }

    private string Merge(string original, string update, bool forceUpdate)
    {
        return _merger.Merge(original, update, forceUpdate);
    }

    private LibrarySource Merge(LibrarySource original, LibrarySource update, bool forceUpdate)
    {
        return _merger.Merge(original, update, forceUpdate);
    }

    private ReleaseType Merge(ReleaseType original, ReleaseType update, bool forceUpdate)
    {
        return _merger.Merge(original, update, forceUpdate);
    }

    private Grouping Merge(Grouping original, Grouping update, bool forceUpdate)
    {
        return _merger.Merge(original, update, forceUpdate);
    }

    private ICollection<T> MergeCollection<T>(ICollection<T> original, ICollection<T> update, bool forceUpdate) where T : IMergeable
    {
        return _merger.MergeCollection(original, update, forceUpdate);
    }

    private List<int> MergeTrackCounts(List<int> original, List<int> update, bool forceUpdate)
    {
        return _merger.MergeTrackCounts(original, update, forceUpdate);
    }
}