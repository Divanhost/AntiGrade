using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
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
    }
}
