using EmployeesApp.Web.Models;

namespace EmployeesApp.Web.Services
{
    public class EmployeeService
    {
        static List<Employee> _employees = new List<Employee> {
        new Employee{Id = 1, Name = "Jonas", Email = "Jonas@testmail.com"},
        new Employee{Id = 2, Name = "Bert", Email = "Bert@testmail.com"},
        new Employee{Id = 3, Name = "Andreas", Email = "Andreas@testmail.com"}
        };

        public void Add(Employee employee)
        {
            employee.Id = _employees.Count > 0 ? _employees.Max(p => p.Id) + 1 : 1;

            _employees.Add(employee);

        }

        public Employee[] GetAll()
        {
            return _employees.ToArray();
        }

        public Employee GetById(int id)
        {
            Employee employee = _employees.Single(e => e.Id == id);
            return employee;
        }

        public bool OnWork(Employee employee)
        {
            if (!employee.OnWork)
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

            // return true om man stämplar in, retrun false, om man stämplar ut

        }

    }
}
