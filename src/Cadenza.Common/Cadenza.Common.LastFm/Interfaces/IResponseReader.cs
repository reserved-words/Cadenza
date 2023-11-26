namespace Cadenza.Common.LastFm.Interfaces;

internal interface IResponseReader
{
    XElement GetXmlContent(string contentAsString);
}