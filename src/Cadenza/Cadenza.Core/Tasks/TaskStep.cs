﻿namespace Cadenza.Common;

public class TaskStep
{
    public string Caption { get; set; }
    public Func<object, Task<object>> Task { get; set; }
}

public class TaskCheckStep
{
    public string Caption { get; set; }
    public Func<Task<bool>> Task { get; set; }
}