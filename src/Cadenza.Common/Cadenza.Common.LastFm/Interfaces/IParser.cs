namespace Cadenza.Common.LastFm.Interfaces;

internal interface IParser
{
    string Get(XElement xml, string propertyName, bool isAttribute = false);
    string Get(XElement xml, string name, string propertyName, bool isAttribute = false);
    bool GetBool(XElement xml, string propertyName, bool isAttribute = false);
    DateTime GetDateTime(XElement xml, string propertyName);
    string GetImage(XElement xml);
    int GetInt(XElement xml, string propertyName, bool isAttribute = false);
}