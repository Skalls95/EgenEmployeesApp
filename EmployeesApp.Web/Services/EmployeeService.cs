using EmployeesApp.Web.Models;

namespace EmployeesApp.Web.Services
{
    public class EmployeeService
    {
        static List<Employee> Employees = new List<Employee> {
        new Employee{Id = 1, Name = "Jonas", Email = "Jonas@testmail.com"},
        new Employee{Id = 2, Name = "Bert", Email = "Bert@testmail.com"},
        new Employee{Id = 3, Name = "Andreas", Email = "Andreas@testmail.com"}
        };

        public void Add(Employee employee)
        {
            Employees.Add(employee);
        }

        public Employee[] GetAll()
        {
            return Employees.ToArray();
        }

        public Employee GetById(int id)
        {
            Employee employee = Employees.Single(e => e.Id == id);
            return employee;
        }

    }
}
