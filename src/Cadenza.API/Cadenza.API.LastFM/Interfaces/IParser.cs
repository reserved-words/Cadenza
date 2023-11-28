namespace Cadenza.API.LastFM.Interfaces;

internal interface IParser
{
    string Get(XElement xml, string propertyName, bool isAttribute = false);
    string Get(XElement xml, string name, string propertyName, bool isAttribute = false);
    string GetImage(XElement xml);
}