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
}
