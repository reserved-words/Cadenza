namespace Cadenza.Common.Interfaces.Utilities;

public interface IJsonConverter
{
    string Serialize<T>(T item);
    T Deserialize<T>(string json) where T : new();
}
