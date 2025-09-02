using EmployeesApp.Web.Services;

namespace EmployeesApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        app.MapControllers();

        app.Run();
    }
}
