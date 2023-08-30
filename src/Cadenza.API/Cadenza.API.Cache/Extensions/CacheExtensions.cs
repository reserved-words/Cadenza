namespace Cadenza.API.Cache.Extensions;

internal static class CacheExtensions
{
    public static void Cache<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
        }
    }

    public static void Cache(this Dictionary<string, List<PlayTrack>> dictionary, TrackInfo track, ArtistInfo artist, AlbumInfo album, PlayTrack playTrack)
    {
        var tags = track.Tags.Tags
            .Concat(artist.Tags.Tags)
            .Concat(album.Tags.Tags)
            .Distinct();

        foreach (var tag in tags)
        {
            if (!dictionary.TryGetValue(tag, out List<PlayTrack> list))
            {
                list = new List<PlayTrack>();
                dictionary.Add(tag, list);
            }

            list.Add(playTrack);
        }
    }

    public static void Cache(this Dictionary<PlayerItemType, Dictionary<string, List<PlayTrack>>> dictionary, PlayerItemType type, string id, PlayTrack playTrack)
    {
        if (!dictionary.ContainsKey(type))
        {
            var innerDictionary = new Dictionary<string, List<PlayTrack>>();
            dictionary.Add(type, innerDictionary);

            if (!innerDictionary.ContainsKey(id))
            {
                innerDictionary.Add(id, new List<PlayTrack>());
            }
        }

        dictionary[type][id].Add(playTrack);
    }


    public static void Cache<T1, T2>(this Dictionary<T1, List<T2>> dictionary, T1 key, T2 value)
    {
        if (!dictionary.TryGetValue(key, out var list))
        {
            list = new List<T2>();
            dictionary.Add(key, list);
        }

        if (list.Contains(value))
            return;

        list.Add(value);
    }

    public static void Cache(this Dictionary<string, List<PlayerItem>> dictionary, TagList tags, PlayerItem item)
    {
        foreach (var tag in tags.Tags)
        {
            dictionary.Cache(tag, item);
        }
    }

    public static void Cache<T1, T2>(this Dictionary<T1, List<T2>> dictionary, T1 key, string id, Func<T2> create) where T2 : PlayerItem
    {
        if (!dictionary.TryGetValue(key, out var list))
        {
            list = new List<T2>();
            dictionary.Add(key, list);
        }

        if (!list.Any(i => i.Id == id))
        {
            list.Add(create());
        }
    }

    public static void Cache<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }

    public static List<T3> GetAllValues<T1, T2, T3>(this Dictionary<T1, T2> dictionary) where T2 : T3
    {
        return dictionary.Values
            .OfType<T3>()
            .ToList();
    }

    public static List<T3> GetList<T1, T2, T3>(this Dictionary<T1, List<T2>> dictionary, T1 key) where T2 : T3
    {
        return dictionary.TryGetValue(key, out List<T2> list)
            ? list.OfType<T3>().ToList()
            : new List<T3>();
    }

    public static T2 GetValue<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key) where T2 : class
    {
        return dictionary.TryGetValue(key, out T2 value)
            ? value
            : null;
    }
}
