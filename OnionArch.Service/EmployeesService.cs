using AutoMapper;
using OnionArch.Data.DTOs;
using OnionArch.Data.Entities;
using OnionArch.Data.Interfaces;
using OnionArch.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Service
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;

        public EmployeesService(IEmployeesRepository employeesRepository,IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            this._mapper = mapper;
        }
        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeesRepository.GetEmployeeByIdAsync(id);

             return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees=await _employeesRepository.GetEmployeesAsync();


               return _mapper.Map<List<Employee>, List<EmployeeDto>>(employees); 
           
        }

        public async Task<List<PositionType>> GetPositionTypesAsync()
        {
            return await _employeesRepository.GetPositionTypesAsync();
        }

        public async Task<bool> PostCreateEmployeeAsync(EmployeeNew employeeDto)
        {
            return await _employeesRepository.PostCreateEmployeeAsync(employeeDto);
        }

        public async Task<bool> PostDeleteEmployeeAsync(Guid id)
        {
          return  await _employeesRepository.PostDeleteEmployeeAsync(id);
        }
        public async Task<bool> PostUpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            return await _employeesRepository.PostUpdateEmployeeAsync(employeeDto);
        }
    }
}
