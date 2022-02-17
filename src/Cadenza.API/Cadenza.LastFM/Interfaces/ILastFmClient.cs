using System.Xml.Linq;

namespace Cadenza.LastFM.Interfaces;

public interface ILastFmClient
{
    Task<T> Get<T>(string url, Func<XElement, T> getValue);
}
