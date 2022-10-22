using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cadenza.Common.Domain.JsonConverters;

public static class JsonSerialization
{
    private static readonly ICollection<JsonConverter> _converters;

    static JsonSerialization()
    {
        _converters = new List<JsonConverter>
        {
           new TagListConverter()
        };
        Options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        SetOptions(Options);
    }

    public static JsonSerializerOptions Options { get; }

    public static void SetOptions(JsonSerializerOptions options)
    {
        foreach (var converter in _converters)
        {
            options.Converters.Add(converter);
        }
    }
}