using EmployeesApp.Web.Models;
using EmployeesApp.Web.Services;
using EmployeesApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Web.Controllers;
public class EmployeesController : Controller
{
    static EmployeeService _employeeService = new EmployeeService();

    [Route("/")]
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

    [HttpGet("/punchclock")]
    public IActionResult Clock()
    {
        Employee[] model = _employeeService.GetAll()
            .Where(e => e.OnWork == true)
            .ToArray();

        return View(model);
    }

    [HttpGet("/punchclock/{id}")]
    public IActionResult Clock(int id)
    {
        Employee? model = _employeeService.GetById(id);
        if (model == null)
        {
            TempData["Error"] = $"Ingen anställd med Id {id} hittades!";
            return RedirectToAction(nameof(Clock));
        }

        bool startedWork = _employeeService.OnWork(model);

        if (startedWork)
            TempData["Message"] = $"{model.Name} stämplades in.";
        else
            TempData["Message"] = $"{model.Name} stämplades ut.";

        return RedirectToAction(nameof(Index));
    }

    [Route("/payroll")]
    public IActionResult Payroll()
    {
        Employee[] employees = _employeeService.GetAll();
        PayrollViewModel[] viewModel = employees
            .Select(e => new PayrollViewModel
            {
                EmployeeId = e.Id,
                Name = e.Name,
                HourlyWage = e.HourlyWage,
                TotalHours = e.Stamps
                    .Where(s => s.End.HasValue)
                    .Sum(s => (s.End!.Value - s.Start).TotalHours),
                TotalSalary = (decimal)e.Stamps
                    .Where(s => s.End.HasValue)
                    .Sum(s => (s.End!.Value - s.Start).TotalHours) * e.HourlyWage
            }).ToArray();

        return View(viewModel);
    }



}
