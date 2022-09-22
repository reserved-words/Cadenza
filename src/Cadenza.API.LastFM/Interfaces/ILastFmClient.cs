using System.Xml.Linq;

namespace Cadenza.API.LastFM.Interfaces;

public interface ILastFmClient
{
    Task<T> Get<T>(string url, Func<XElement, T> getValue);
}
