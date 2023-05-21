namespace Cadenza.API.LastFM.Services;

internal class ResponseReader : IResponseReader
{
    public XElement GetXmlContent(string contentAsString)
    {
        var xml = XDocument.Parse(contentAsString);

        var root = xml.Element("lfm");
        if (root.Attribute("status").Value == "failed")
        {
            var error = root.Element("error");
            throw new Exception(error.Value);
        }

        return root;
    }
}