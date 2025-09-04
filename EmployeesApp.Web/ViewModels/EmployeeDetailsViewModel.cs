using EmployeesApp.Web.Models;

namespace EmployeesApp.Web.ViewModels;

public class EmployeeDetailsViewModel
{
    public Employee Employee { get; set; }
    public string QrCodeBase64 { get; set; }

}
