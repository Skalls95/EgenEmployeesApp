using EmployeesApp.Web.Models;
using EmployeesApp.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Web.Controllers;
public class EmployeesController : Controller
{
    EmployeeService employeeService = new EmployeeService();

    [Route("")]
    public IActionResult Index()
    {
        Employee[] model = employeeService.GetAll();
        return View(model);
    }

    [HttpGet("/create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("/create")]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid)
            return View();

        if (employee.Name == "Håkan")
        {
            ModelState.AddModelError(nameof(employee.Name), "Håkan är bannlyst");
        }

        employeeService.Add(employee);

        return RedirectToAction(nameof(Index));
    }

}
