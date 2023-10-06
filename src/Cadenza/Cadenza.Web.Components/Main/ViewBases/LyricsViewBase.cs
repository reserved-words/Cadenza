namespace Cadenza.Web.Components.Main.ViewBases;

public class LyricsViewBase : TrackViewBase
{
    public MarkupString Lyrics => (MarkupString)Model.Lyrics.WithLineBreaks();
}
