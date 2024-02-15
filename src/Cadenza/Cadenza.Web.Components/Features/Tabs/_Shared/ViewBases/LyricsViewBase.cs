namespace Cadenza.Web.Components.Features.Tabs._Shared.ViewBases;

public class LyricsViewBase : TrackViewBase
{
    public MarkupString Lyrics => (MarkupString)Model.Lyrics.WithLineBreaks();
}
