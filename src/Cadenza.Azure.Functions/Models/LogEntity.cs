using Microsoft.Azure.Cosmos.Table;
using System;

namespace Cadenza.Azure.Functions.Models;

public class LogEntity : TableEntity
{
    public DateTime DateCreated { get; set; }
    public string Message { get; set; }
    public bool Cleared { get; set; }
    public int Level { get; set; }
    public string StackTrace { get; set; }
}
