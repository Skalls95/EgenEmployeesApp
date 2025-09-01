using EmployeesApp.Web.Models;
using EmployeesApp.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Web.Controllers;
public class EmployeesController : Controller
{
    static EmployeeService _employeeService = new EmployeeService();

    [Route("")]
    public IActionResult Index()
    {
        Employee[] model = _employeeService.GetAll();
        return View(model);
    }

    [HttpGet("/employee/{id}")]
    public IActionResult Details(int id)
    {
        var model = _employeeService.GetById(id);
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

        _employeeService.Add(employee);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("stamp")]
    public IActionResult Clock()
    {
        Employee[] model = _employeeService.GetAll()
            .Where(e => e.OnWork == true)
            .ToArray();

        return View(model);
    }

    [HttpPost("stamp")]
    public IActionResult Clock(int id)
    {
        Employee? employee = _employeeService.GetById(id);
        if (employee == null)
        {
            TempData["Error"] = $"Ingen anställd med Id {id} hittades!";
            return RedirectToAction(nameof(Clock));
        }

        bool startedWork = _employeeService.OnWork(employee);

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
