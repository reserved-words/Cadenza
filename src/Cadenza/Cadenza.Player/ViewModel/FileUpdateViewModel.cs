using Cadenza.Domain;

namespace Cadenza.Player;

public class FileUpdateViewModel
{
    public FileUpdateViewModel(FileUpdate update)
    {
        Update = update.Update;

        if (update.FailedAttempts == null || !update.FailedAttempts.Any())
            return;

        var lastFailure = update.FailedAttempts.OrderBy(u => u.Date).Last();
        LastFailedAttempt = lastFailure.Date;
        LastError = lastFailure.Error;
        NumberOfFailedAttempts = update.FailedAttempts.Count;
    }

    public ItemPropertyUpdate Update { get; }
    public int NumberOfFailedAttempts { get; }
    public DateTime? LastFailedAttempt { get; }
    public string LastError { get; }
}
