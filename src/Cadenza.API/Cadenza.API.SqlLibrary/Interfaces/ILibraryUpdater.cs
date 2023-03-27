namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryUpdater
{
    Task RemoveChildlessItems();
}