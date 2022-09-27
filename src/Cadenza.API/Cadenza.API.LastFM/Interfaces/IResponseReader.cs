using System.Xml.Linq;

namespace Cadenza.API.LastFM.Interfaces;

internal interface IResponseReader
{
    Task<XElement> GetXmlContent(HttpResponseMessage response);
}