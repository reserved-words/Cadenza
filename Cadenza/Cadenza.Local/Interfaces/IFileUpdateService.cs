namespace Cadenza.Local;

public interface IFileUpdateService
{
    void Add(MetaDataUpdate update);
    FileUpdateQueue Get();
    void LogError(MetaDataUpdate update, Exception ex);
    void Remove(MetaDataUpdate update);
}
