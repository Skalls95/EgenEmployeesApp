using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Employee
{
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal HourlyWage { get; set; }

    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
