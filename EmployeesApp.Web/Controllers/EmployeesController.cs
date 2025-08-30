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

    [HttpGet("/employee/{id}")]
    public IActionResult Details(int id)
    {
        var model = employeeService.GetById(id);
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

        employeeService.Add(employee);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("stamp")]
    public IActionResult Clock()
    {
        Employee[] model = employeeService.GetAll()
            .Where(e => e.OnWork == true)
            .ToArray();

        return View(model);
    }

    [HttpPost("stamp")]
    public IActionResult Clock(int id)
    {
        Employee? employee = employeeService.GetById(id);
        if (employee == null)
        {
            TempData["Error"] = $"Ingen anställd med Id {id} hittades!";
            return RedirectToAction(nameof(Clock));
        }

        bool startedWork = employeeService.OnWork(employee);

        if (startedWork)
            TempData["Message"] = $"{employee.Name} stämplades in.";
        else
            TempData["Message"] = $"{employee.Name} stämplades ut.";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Payroll()
    {
        return View();
    }

}
