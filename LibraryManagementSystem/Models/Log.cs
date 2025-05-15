using System;
using System.ComponentModel.DataAnnotations;

public class Log
{
    [Key]
    public int LogId { get; set; }
    public string UserName { get; set; }
    public string TableName { get; set; }
    public string OperationType { get; set; } 
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
