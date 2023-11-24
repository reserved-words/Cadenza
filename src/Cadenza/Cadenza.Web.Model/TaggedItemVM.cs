namespace Cadenza.Web.Model;

public record TaggedItemVM(PlayerItemType Type, int Id, string Name, string Artist, string Album, string AlbumDisplay);