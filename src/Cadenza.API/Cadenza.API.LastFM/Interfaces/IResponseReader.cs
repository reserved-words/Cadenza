namespace Cadenza.API.LastFM.Interfaces;

internal interface IResponseReader
{
    XElement GetXmlContent(string contentAsString);
}