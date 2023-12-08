namespace Cadenza.Web.Common.Model;

public class EditableArtistRelease
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public string Year { get; set; }
}