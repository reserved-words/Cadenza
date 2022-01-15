using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cadenza.Utilities;

public class JsonConverter : IJsonConverter
{
    private static JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = new List<Newtonsoft.Json.JsonConverter> { new StringEnumConverter() }
    };

    public string Serialize<T>(T item)
    {
        return JsonConvert.SerializeObject(item, _settings);
    }

    public T Deserialize<T>(string json) where T : new()
    {
        if (string.IsNullOrWhiteSpace(json))
            return new T();

        return JsonConvert.DeserializeObject<T>(json, _settings);
    }
}