﻿namespace Cadenza.Source.Spotify;

public class SpotifyOverrides : IStaticSource
{
    private readonly IOverridesService _service;

    public SpotifyOverrides(IOverridesService service)
    {
        _service = service;
    }

    private class Identifier
    {
        public ItemType Type { get; set; }
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Identifier identifier))
                return false;

            return identifier.Type == Type && identifier.Id == Id;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Id.GetHashCode();
        }
    }

    public async Task<StaticLibrary> GetStaticLibrary()
    {
        var overrides = await _service.GetOverrides();

        var library = new StaticLibrary();

        var itemOverrides = overrides
            .GroupBy(i => new Identifier { Type = i.ItemType, Id = i.Id });

        foreach (var grp in itemOverrides)
        {
            if (grp.Key.Type == ItemType.Artist)
            {
                library.Artists.Add(GetArtist(grp));
            }
            else if (grp.Key.Type == ItemType.Album)
            {
                library.Albums.Add(GetAlbum(grp));
            }
            else if (grp.Key.Type == ItemType.Track)
            {
                library.Tracks.Add(GetTrack(grp));
            }
        }

        return library;

    }

    private ArtistInfo GetArtist(IGrouping<Identifier, MetaDataUpdate> overrides)
    {
        return new ArtistInfo
        {
            Id = overrides.Key.Id,
            Name = GetValue(overrides, ItemProperty.Artist),
            Grouping = GetGroupingValue(overrides),
            Genre = GetValue(overrides, ItemProperty.Genre),
            City = GetValue(overrides, ItemProperty.City),
            State = GetValue(overrides, ItemProperty.State),
            Country = GetValue(overrides, ItemProperty.Country),
            Links = new List<Link>() // overrides to do
        };
    }

    private AlbumInfo GetAlbum(IGrouping<Identifier, MetaDataUpdate> overrides)
    {
        return new AlbumInfo
        {
            Id = overrides.Key.Id,
            ArtistId = GetValue(overrides, ItemProperty.AlbumArtist),
            Title = GetValue(overrides, ItemProperty.AlbumTitle),
            ReleaseType = GetReleaseTypeValue(overrides),
            Year = GetValue(overrides, ItemProperty.ReleaseYear)
        };
    }

    private TrackInfo GetTrack(IGrouping<Identifier, MetaDataUpdate> overrides)
    {
        return new TrackInfo
        {
            Id = overrides.Key.Id,
            ArtistId = GetValue(overrides, ItemProperty.Artist),
            Title = GetValue(overrides, ItemProperty.AlbumTitle),
            Lyrics = GetValue(overrides, ItemProperty.Lyrics),
            Year = GetValue(overrides, ItemProperty.ReleaseYear),
            Tags = new List<Tag>() // overrides to do
        };
    }

    private string GetValue(IGrouping<Identifier, MetaDataUpdate> overrides, ItemProperty ItemProperty)
    {
        return overrides.SingleOrDefault(a => a.Property == ItemProperty)?.UpdatedValue;
    }

    private Grouping GetGroupingValue(IGrouping<Identifier, MetaDataUpdate> overrides)
    {
        return overrides.SingleOrDefault(a => a.Property == ItemProperty.Grouping)?.UpdatedValue.Parse<Grouping>()
                        ?? Grouping.None;
    }

    private ReleaseType GetReleaseTypeValue(IGrouping<Identifier, MetaDataUpdate> overrides)
    {
        return overrides.SingleOrDefault(a => a.Property == ItemProperty.ReleaseType)?.UpdatedValue.Parse<ReleaseType>()
                        ?? ReleaseType.Album;
    }
}