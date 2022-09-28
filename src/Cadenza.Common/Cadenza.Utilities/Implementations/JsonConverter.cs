using Cadenza.Utilities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cadenza.Utilities.Implementations;

internal class JsonConverter : IJsonConverter
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

        json = json.Trim();

        try
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
        catch (Exception)
        {
            return new T();
        }
    }
}