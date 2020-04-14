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
    public class WorkController : BaseServiceController<IWorkService>
    {
        public WorkController(IWorkService workService) : base(workService)
        { }

        // [HttpGet("criteria")]
        // public async Task<IActionResult> GetStudentCriteria([FromQuery] List<int> studentIds)
        // {
        //     var result = await _service.GetStudentCriteria(studentIds);
        //     return ResponseModel(result);
        // }
        // [HttpPut("criteria")]
        // public async Task<IActionResult> UpdateStudentCriteria([FromBody] List<StudentCriteria> studentCriteria)
        // {
        //     var result = await _service.UpdateStudentCriteria(studentCriteria);
        //     return ResponseModel(result);
        // }

        [HttpGet("studentworks/{id:int}")]
        public async Task<IActionResult> GetStudentWorks([FromQuery] List<int> workIds, int id)
        {
            var result = await _service.GetStudentWorks(id,workIds);
            return ResponseModel(result);
        }

        [HttpPut("studentworks")]
        public async Task<IActionResult> UpdateStudentWorks([FromBody] List<StudentWork> studentWorks)
        {
            var result = await _service.UpdateStudentWorks(studentWorks);
            return ResponseModel(result);
        }
    }
}
