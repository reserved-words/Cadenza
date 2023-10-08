using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cadenza.Common.Utilities.Serialization;

public static class JsonSerialization
{
    private static readonly ICollection<JsonConverter> _converters;

    static JsonSerialization()
    {
        _converters = new List<JsonConverter>
        {
           new JsonStringEnumConverter()
        };
        Options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        SetOptions(Options);
    }

    public static JsonSerializerOptions Options { get; }

    public static void SetOptions(JsonSerializerOptions options)
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.WriteIndented = true;
        foreach (var converter in _converters)
        {
            options.Converters.Add(converter);
        }
    }
}