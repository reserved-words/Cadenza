namespace Cadenza.Web.Database.Services;

internal class DataTransferObjectMapper : IDataTransferObjectMapper
{
    public ItemUpdateRequestDTO Map(AlbumUpdateVM vm)
    {
        return new ItemUpdateRequestDTO
        {
            Id = vm.Id,
            Type = vm.Type,
            Updates = vm.Updates.Select(u => Map(u)).ToList()
        };
    }

    public ItemUpdateRequestDTO Map(ArtistUpdateVM vm)
    {
        return new ItemUpdateRequestDTO
        {
            Id = vm.Id,
            Type = vm.Type,
            Updates = vm.Updates.Select(u => Map(u)).ToList()
        };
    }

    public ItemUpdateRequestDTO Map(TrackUpdateVM vm)
    {
        return new ItemUpdateRequestDTO
        {
            Id = vm.Id,
            Type = vm.Type,
            Updates = vm.Updates.Select(u => Map(u)).ToList()
        };
    }

    private PropertyUpdateDTO Map(PropertyUpdateVM vm)
    {
        return new PropertyUpdateDTO 
        { 
            Id = vm.Id,
            Property = vm.Property,
            OriginalValue = vm.OriginalValue, 
            UpdatedValue = vm.UpdatedValue 
        };
    }
}
