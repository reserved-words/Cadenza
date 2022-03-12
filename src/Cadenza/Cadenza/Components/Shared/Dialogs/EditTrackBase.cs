//using Cadenza.Core.Updates;

//namespace Cadenza;

//public class EditTrackBase : FormBase<TrackUpdate>
//{
//    private const string LyricsSearchUrlFormat = "https://www.google.com/search?q={0}";
//    private const string LyricsSearchQueryFormat = @"""{0}"" ""{1}"" lyrics";

//    public string LyricsSearchUrl => ConstructLyricsSearchUrl();

//    private string ConstructLyricsSearchUrl()
//    {
//        var query = string.Format(LyricsSearchQueryFormat, Model.Item.ArtistName, Model.Item.Title);
//        var encodedQuery = HttpUtility.UrlEncode(query);
//        return string.Format(LyricsSearchUrlFormat, encodedQuery);
//    }
//}