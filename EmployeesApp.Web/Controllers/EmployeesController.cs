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
        // Gör en box där jag kan skriva in id på den som ska stämplas ut/in
        // Visa alla personer via en dropdown, om de är instämplade så ska de ha en knapp för att stämpla ut och reverse.

        Employee[] model = employeeService.GetAll();

        return View(model);
    }

    [HttpPost("stamp")]
    public IActionResult Clock(int id)
    {
        // skicka tillbaka personen om id ej hittades

        // Om man lyckas stämpla ut/in, skicka till start om man lyckas med konfirmation.

        // Ta id på personen som vill stämpla in/ut och spara tiden som de stämplade
        return View();
    }

}
