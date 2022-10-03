﻿namespace Cadenza.Common.Interfaces.Utilities;

public interface INameComparer
{
    string GetCompareName(string name);

    string GetSortName(string name);

    string GetStandardisedName(string name);
}