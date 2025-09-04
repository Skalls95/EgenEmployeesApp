using EmployeesApp.Web.Models;
using EmployeesApp.Web.Services;
using EmployeesApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QRCoder;
using System.Reflection;

namespace EmployeesApp.Web.Controllers;
public class EmployeesController : Controller
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [Route("/")]
    public IActionResult Index()
    {
        Employee[] model = _service.GetAll();
        return View(model);
    }

    [HttpGet("/employee/{id}")]
    public IActionResult Details(int id)
    {
        var model = _service.GetById(id);
        if (model == null)
        {
            TempData["Error"] = $"Ingen anställd med Id {id} hittades!";
            return RedirectToAction(nameof(Index));
        }

        var url = Url.Action(
            nameof(Clock), 
            "Employees", 
            new { id }, 
            Request.Scheme);

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(5);

        var viewModel = new EmployeeDetailsViewModel
        {
            Employee = model,
            QrCodeBase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}"
        };

        return View(viewModel);
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

        _service.Add(employee);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/punchclock")]
    public IActionResult Clock(int? id)
    {
        if (id.HasValue)
        {
            Employee? model = _service.GetById(id.Value);
            if (model == null)
            {
                TempData["Error"] = $"Ingen anställd med Id {id} hittades!";
                return RedirectToAction(nameof(Clock));
            }

            bool startedWork = _service.OnWork(model);

            TempData["Message"] = startedWork
                ? $"{model.Name} stämplades in."
                : $"{model.Name} stämplades ut.";

            return RedirectToAction(nameof(Index));
        }

        Employee[] modelList =  _service.GetAll()
            .Where(e => e.OnWork == true)
            .ToArray();

        return View(modelList);
    }

    [Route("/payroll")]
    public IActionResult Payroll()
    {
        Employee[] employees = _service.GetAll();
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
