namespace Cadenza.API.Core.Services.Cache;

internal static class CacheExtensions
{
    public static void Cache<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
    {
        if (!dictionary.TryGetValue(key, out var item))
        {
            dictionary.Add(key, value);
        }
    }

    public static void Cache(this Dictionary<PlayerItemType, Dictionary<string, List<PlayTrack>>> dictionary, PlayerItemType type, string id, PlayTrack playTrack)
    {
        if (!dictionary.TryGetValue(type, out Dictionary<string, List<PlayTrack>> innerDictionary))
        {
            innerDictionary = new Dictionary<string, List<PlayTrack>>();
            dictionary.Add(type, innerDictionary);

            if (!innerDictionary.TryGetValue(id, out List<PlayTrack> list))
            {
                list = new List<PlayTrack>();
                innerDictionary.Add(id, list);
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
}
