using EmployeesApp.Web.Models;

namespace EmployeesApp.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        //public List<Employee> Employees = new List<Employee> {
        //new Employee{Id = 1, Name = "Jonas", Email = "Jonas@testmail.com"},
        //new Employee{Id = 2, Name = "Bert", Email = "Bert@testmail.com"},
        //new Employee{Id = 3, Name = "Andreas", Email = "Andreas@testmail.com"}
        //};

        public List<Employee> Employees = TestDataService.GetEmployees();

        public void Add(Employee employee)
        {
            employee.Id = Employees.Count > 0 ? Employees.Max(p => p.Id) + 1 : 1;

            Employees.Add(employee);
        }

        public Employee[] GetAll()
        {
            return Employees.ToArray();
        }

        public Employee? GetById(int id)
        {
            Employee? employee = Employees.SingleOrDefault(e => e.Id == id);
            return employee;
        }

        public bool OnWork(Employee employee)
        {
            if (!employee.OnWork || employee.Stamps.Count == 0)
            {
                employee.Stamps.Add(new StampTime() { Start =  DateTime.Now });
                employee.OnWork = true;
                return true;
            }
            else
            {
                employee.Stamps.Last().End = DateTime.Now;
                employee.OnWork = false;
                return false;
            }
        }
    }
}
