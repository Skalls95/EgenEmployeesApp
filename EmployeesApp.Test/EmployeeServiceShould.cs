using EmployeesApp.Web.Models;
using EmployeesApp.Web.Services;

namespace EmployeesApp.Test;

public class EmployeeServiceShould
{

    [Fact]
    public void AddNewEmployee_ShouldIncreaseCount_AndAssignID()
    {
        EmployeeService employeeService = new EmployeeService();
        int beforeAddingCount = EmployeeService.Employees.Count;
        int previusMaxId = EmployeeService.Employees.Max(e => e.Id);

        Employee sut = new Employee() { Email = "simon@testmail.com", Name = "Simon" };

        employeeService.Add(sut);

        Assert.Contains(sut, EmployeeService.Employees);
        Assert.Equal(beforeAddingCount +1, EmployeeService.Employees.Count);
        Assert.Equal(previusMaxId + 1, EmployeeService.Employees.Max(e => e.Id));
    }

    [Fact]
    public void GetEmployeeById()
    {

    }

    [Fact]
    public void GetAllEmployees()
    {

    }

    [Fact]
    public void ClockEmployee()
    {

    }
}
