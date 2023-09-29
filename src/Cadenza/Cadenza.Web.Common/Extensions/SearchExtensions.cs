namespace Cadenza.Web.Common.Extensions;

public static class SearchExtensions
{
    public static bool IsCommon(this string searchTerm)
    {
        return searchTerm.Equals("the", StringComparison.InvariantCultureIgnoreCase)
            || searchTerm.Equals("the ", StringComparison.InvariantCultureIgnoreCase);
    }
}
