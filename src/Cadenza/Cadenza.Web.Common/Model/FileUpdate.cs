using Cadenza.Domain.Models;

namespace Cadenza.Web.Common.Model;

public class FileUpdate
{
    public ItemPropertyUpdate Update { get; set; }
    public List<FileUpdateFailedAttempt> FailedAttempts { get; set; } = new();
}
