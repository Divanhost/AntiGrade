using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AntiGrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseServiceController<IStudentService>
    {
        public StudentController(IStudentService studentService) : base(studentService)
        { }

        [HttpPost()]
        public async Task<IActionResult> GetStudentCriteria([FromQuery] List<int> studentIds)
        {
            var result = await _service.GetStudentCriteria(studentIds);
            return ResponseModel(result);
        }
    }
}
