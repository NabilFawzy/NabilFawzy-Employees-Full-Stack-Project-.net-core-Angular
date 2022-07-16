
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArch.Data.DTOs;
using OnionArch.Data.Entities;
using OnionArch.Data.Interfaces;
using OnionArch.Repository.Data;
using OnionArch.Repository.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMapper mapper;

        public EmployeesController(IEmployeesService employeesService,IMapper mapper)
        {
           _employeesService = employeesService;
            this.mapper = mapper;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task< ActionResult<List<EmployeeDto>>> GetEmployees()
        {
            var employees =await _employeesService.GetEmployeesAsync();
            return  Ok(employees);
            
        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<PositionType>>> GetPositionTypes()
        {
            var positions = await _employeesService.GetPositionTypesAsync();
            return Ok(positions);

        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task< EmployeeDto> GetEmployee(Guid id)
        {

            return await _employeesService.GetEmployeeByIdAsync(id);
        }

     
        [HttpPost("UpdateEmployee")]
        public async Task<Boolean> UpdateEmployee(EmployeeDto employeeDto)
        {

            return await _employeesService.PostUpdateEmployeeAsync(employeeDto);
        }

        [HttpPost("CreateEmployee")]
        public async Task<Boolean> CreateEmployee(EmployeeNew employeeDto)
        {

            return await _employeesService.PostCreateEmployeeAsync(employeeDto);
        }

        [HttpGet("DeleteEmployee/{id}")]
        public async Task<Boolean> DeleteEmployee(Guid id)
        {

            return await _employeesService.PostDeleteEmployeeAsync(id);
        }


    }
}
