using Microsoft.EntityFrameworkCore;
using OnionArch.Data.DTOs;
using OnionArch.Data.Entities;
using OnionArch.Data.Interfaces;
using OnionArch.Repository.Data;
using OnionArch.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeContext _context;

        public EmployeesRepository(EmployeeContext context)
        {
            this._context = context;
        }
        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees
                .Include(x => x.PositionType)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees
                 .Include(x => x.PositionType)
                .ToListAsync();
        }

        public async Task<List<PositionType>> GetPositionTypesAsync()
        {
            return await _context.PositionTypes.ToListAsync();
        }

        public async Task<bool> PostCreateEmployeeAsync(EmployeeNew employeeDto)
        {
            try
            {
                Guid newGuid = Guid.NewGuid();
                Employee employee = new Employee();
                employee.FullName = employeeDto.FullName;
                employee.UserName = employeeDto.UserName;
                employee.PositionTypeId = employeeDto.PositionTypeId;
                employee.Job = employeeDto.Job;
                employee.Email = employeeDto.Email;
                employee.isAdmin = employeeDto.isAdmin;
                employee.Id = newGuid;
                employee.Password = employeeDto.Password;

                await _context.Employees.AddAsync(employee);

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> PostDeleteEmployeeAsync(Guid id)
        {
            var deleteuser = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (deleteuser == null)
            {
                return false;
            }
            else
            {
                _context.Remove(deleteuser);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> PostUpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var emp=await _context.Employees.FirstOrDefaultAsync(x=>x.Id == employeeDto.Id);

            if (emp == null) return false;

            try
            {
                emp.FullName = employeeDto.FullName;
                emp.UserName = employeeDto.UserName;
                emp.Email = employeeDto.Email;
                emp.PositionTypeId = employeeDto.PositionTypeId;
                emp.Job = employeeDto.Job;

                _context.Employees.Update(emp);

                var t =await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
    

