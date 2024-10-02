using System;

namespace SmartInventoryBE.Models;

public class Log
{
    public int LogId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public string Action { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; }
    public string Description { get; set; } = string.Empty;
}
