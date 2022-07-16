using OnionArch.Data.DTOs;
using OnionArch.Data.Entities;
using OnionArch.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Data.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<PositionType>> GetPositionTypesAsync();

        Task<Boolean> PostUpdateEmployeeAsync(EmployeeDto employeeDto);
        Task<Boolean> PostCreateEmployeeAsync(EmployeeNew employeeDto);
        Task<Boolean> PostDeleteEmployeeAsync(Guid id);

    }
}
