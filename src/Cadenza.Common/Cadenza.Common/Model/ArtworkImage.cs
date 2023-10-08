namespace Cadenza.Common.Model;

public class ArtworkImage
{

    public ArtworkImage(byte[] bytes, string mimeType)
    {
        Bytes = bytes;
        MimeType = mimeType;
    }

    public byte[] Bytes { get; private set; }
    public string MimeType { get; private set; }
}
