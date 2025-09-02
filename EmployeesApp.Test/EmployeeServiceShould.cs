using EmployeesApp.Web.Models;
using EmployeesApp.Web.Services;

namespace EmployeesApp.Test;

public class EmployeeServiceShould
{

    [Fact]
    public void Add_ShouldIncreaseCount_AndAssignId()
    {
        EmployeeService sut = new EmployeeService();

        int beforeAddingCount = sut.Employees.Count;
        int previousMaxId = sut.Employees.Max(e => e.Id);
        Employee newEmployee = new Employee { Email = "simon@testmail.com", Name = "Simon" };

        sut.Add(newEmployee);

        Assert.Contains(newEmployee, sut.Employees);
        Assert.Equal(beforeAddingCount + 1, sut.Employees.Count);
        Assert.Equal(previousMaxId + 1, sut.Employees.Max(e => e.Id));
    }

    [Fact]
    public void Add_FirstShouldStartWithId1_WhenListIsEmpty()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee firstEmployee = new Employee() { Email = "simon@testmail.com", Name = "Simon" };

        sut.Add(firstEmployee);
        
        Assert.Equal(1, firstEmployee.Id);
    }

    [Fact]
    public void GetById_ShouldReturnCorrectEmployee_WhenEmployeeExists()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee otherEmployee1 = new Employee { Email = "lisa@testmail.com", Name = "Lisa" };
        Employee targetEmployee = new Employee { Email = "gunnar@testmail.com", Name = "Gunnar" };
        Employee otherEmployee2 = new Employee { Email = "anders@testmail.com", Name = "Anders" };
        sut.Add(otherEmployee1);
        sut.Add(targetEmployee);
        sut.Add(otherEmployee2);

        Employee? result = sut.GetById(targetEmployee.Id);

        Assert.NotNull(result);
        Assert.Equal("Gunnar", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenIdDoesntExist()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee existingEmployee = new Employee { Email = "test@testmail.com", Name = "Test" };
        sut.Add(existingEmployee);

        int nonExistingId = existingEmployee.Id + 10;
        Employee? result = sut.GetById(nonExistingId);

        Assert.Null(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAll()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee lisa = new Employee { Email = "Lisa@testmail.com", Name = "Lisa" };
        Employee gunnar = new Employee { Email = "gunnar@testmail.com", Name = "Gunnar" };
        Employee anders = new Employee { Email = "anders@testmail.com", Name = "Anders" };
        sut.Add(lisa);
        sut.Add(gunnar);
        sut.Add(anders);

        Employee[] targetArray = [lisa, gunnar, anders];
        Employee[] result = sut.GetAll();

        Assert.All(targetArray, e => Assert.Contains(e, result));
    }

    [Fact]
    public void OnWork_ShouldClockInEmployee_AndCreateNewStamp()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee newEmployee = new Employee { Email = "simon@testmail.com", Name = "Simon" };
        sut.Add(newEmployee);
        
        int previousStampsCount = newEmployee.Stamps.Count;
        bool isClockedIn = sut.OnWork(newEmployee);

        Assert.Equal(previousStampsCount + 1, newEmployee.Stamps.Count);
        Assert.True(isClockedIn);
        Assert.True(newEmployee.OnWork);
        Assert.NotEqual(default(DateTime), newEmployee.Stamps[0].Start);
        Assert.Null(newEmployee.Stamps[0].End);
    }

    [Fact]
    public void OnWork_ShouldClockOutEmployee_AndSetEndTime()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee newEmployee = new Employee { Email = "simon@testmail.com", Name = "Simon" };
        sut.Add(newEmployee);

        sut.OnWork(newEmployee);
        bool isClockedIn = sut.OnWork(newEmployee);

        Assert.False(isClockedIn);
        Assert.False(newEmployee.OnWork);
        Assert.NotNull(newEmployee.Stamps[0].End);
    }

    [Fact]
    public void OnWork_ShouldSetStartAndEndTimeToNow_WhenClockinginOrOut()
    {
        EmployeeService sut = new EmployeeService();
        sut.Employees.Clear();
        Employee newEmployee = new Employee { Email = "simon@testmail.com", Name = "Simon" };
        sut.Add(newEmployee);

        var beforeClockIn = DateTime.Now.AddMinutes(-1);
        sut.OnWork(newEmployee);
        var afterClockIn = DateTime.Now.AddMinutes(1);

        var beforeClockOut = DateTime.Now.AddMinutes(-1);
        sut.OnWork(newEmployee);
        var afterClockOut = DateTime.Now.AddMinutes(1);

        var stamp = newEmployee.Stamps[0];

        Assert.InRange(stamp.Start, beforeClockIn, afterClockIn);
        Assert.InRange(stamp.End!.Value, beforeClockOut, afterClockOut);
    }
}
