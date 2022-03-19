namespace Cadenza.Local.API;

public static class ApiResults
{
    public static IResult Stream(this string path)
    {
        Stream stream = new FileStream(path, FileMode.Open);

        if (stream == null)
            return null;

        return Results.File(stream, "audio/mpeg");
    }

    public static async Task WriteArtwork(this HttpContext context, (byte[] Bytes, string Type) artwork)
    {
        context.Response.ContentType = artwork.Type;
        context.Response.ContentLength = artwork.Bytes.Length;
        await context.Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(artwork.Bytes));
    }
}
