using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repositories;
public class EmployeeRepository : IEmployeeRepository
{
    public Task DeleteAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<List<Employee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Employee employee)
    {
        return CreateAsync(employee);
    }
}
