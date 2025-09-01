namespace EmployeesApp.Web.ViewModels;

public class PayrollViewModel
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal HourlyWage { get; set; }
    public double TotalHours { get; set; }
    public decimal TotalSalary { get; set; }
}
