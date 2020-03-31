using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared.InputModels;
using AntiGrade.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AntiGrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseServiceController<IEmployeeService>
    {
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        { }
        [HttpGet("teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _service.GetAllTeachers();
            return ResponseModel(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _service.GetEmployeeById(id);
            return ResponseModel(result);
        }
        [HttpGet("positions")]
        public async Task<IActionResult> GetEmployeePositions()
        {
            var result = await _service.GetEmployeePositions();
            return ResponseModel(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var result = await _service.CreateEmployee(employeeDto);
            return ResponseModel(result);
        }
    }
}
