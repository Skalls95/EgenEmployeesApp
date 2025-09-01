using EmployeesApp.Web.Models;
using Bogus;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
namespace EmployeesApp.Web.Services;

public static class TestDataService
{
    public static List<Employee> GetEmployees(int count = 5)
    {
        var stampFaker = new Faker<StampTime>("sv")
            .RuleFor(s => s.Start, f => f.Date.Recent(10))
            .RuleFor(s => s.End, (f, s) => s.Start.AddHours(f.Random.Int(4, 8)));

        var employees = new Faker<Employee>("sv")
            .RuleFor(e => e.Id, f => f.IndexFaker + 1)
            .RuleFor(e => e.Name, f => f.Name.FullName())
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.HourlyWage, f => f.Random.Int(100, 200))
            .RuleFor(e => e.Stamps, f => stampFaker.Generate(f.Random.Int(1, 5)))
            .Generate(count);

        return employees;
    }
}
