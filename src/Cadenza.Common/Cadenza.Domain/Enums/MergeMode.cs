namespace Cadenza.Domain;

public enum MergeMode
{
    ReplaceIfOriginalIsEmpty,
    ReplaceIfUpdateIsNotEmpty,
    ReplaceAlways 
}
