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

    public static void Cache(this Dictionary<string, List<int>> dictionary, TrackDetailsDTO track, ArtistDetailsDTO artist, AlbumDetailsDTO album, int trackId)
    {
        var tags = track.Tags.Tags
            .Concat(artist.Tags.Tags)
            .Concat(album.Tags.Tags)
            .Distinct();

        foreach (var tag in tags)
        {
            if (!dictionary.TryGetValue(tag, out List<int> list))
            {
                list = new List<int>();
                dictionary.Add(tag, list);
            }

            list.Add(trackId);
        }
    }

    public static void Cache(this Dictionary<PlayerItemType, Dictionary<string, List<int>>> dictionary, PlayerItemType type, string id, int trackId)
    {
        if (!dictionary.ContainsKey(type))
        {
            var innerDictionary = new Dictionary<string, List<int>>();
            dictionary.Add(type, innerDictionary);

            if (!innerDictionary.ContainsKey(id))
            {
                innerDictionary.Add(id, new List<int>());
            }
        }

        dictionary[type][id].Add(trackId);
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

    public static void Cache(this Dictionary<string, List<PlayerItemDTO>> dictionary, List<string> tags, PlayerItemDTO item)
    {
        foreach (var tag in tags)
        {
            dictionary.Cache(tag, item);
        }
    }

    public static void Cache<T1, T2>(this Dictionary<T1, List<T2>> dictionary, T1 key, string id, Func<T2> create) where T2 : PlayerItemDTO
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
