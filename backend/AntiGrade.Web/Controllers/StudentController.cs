using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntiGrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StudentController : BaseServiceController<IStudentService>
    {
        public StudentController(IStudentService studentService) : base(studentService)
        { }

        [HttpGet("criteria")]
        public async Task<IActionResult> GetStudentCriteria([FromQuery] List<int> studentIds)
        {
            var result = await _service.GetStudentCriteria(studentIds);
            return ResponseModel(result);
        }
        [HttpPut("criteria")]
        public async Task<IActionResult> UpdateStudentCriteria([FromBody] List<StudentCriteria> studentCriteria)
        {
            var result = await _service.UpdateStudentCriteria(studentCriteria);
            return ResponseModel(result);
        }

        [HttpGet("works")]
        public async Task<IActionResult> GetStudentWorks([FromQuery] List<int> studentIds)
        {
            var result = await _service.GetStudentWorks(studentIds);
            return ResponseModel(result);
        }

        [HttpPut("works")]
        public async Task<IActionResult> UpdateStudentWorks([FromBody] List<StudentWork> studentWorks)
        {
            var result = await _service.UpdateStudentWorks(studentWorks);
            return ResponseModel(result);
        }
    }
}
