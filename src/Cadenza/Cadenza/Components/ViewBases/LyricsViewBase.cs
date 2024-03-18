namespace Cadenza.Components.ViewBases;

public class LyricsViewBase : TrackViewBase
{
    public MarkupString Lyrics => (MarkupString)Model.Lyrics.WithLineBreaks();
}
