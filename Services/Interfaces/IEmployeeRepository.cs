using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces;
public interface IEmployeeRepository
{
    Task CreateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task UpdateAsync(Employee employee);
}
