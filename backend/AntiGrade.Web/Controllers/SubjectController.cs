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
    public class SubjectController : BaseServiceController<ISubjectService>
    {
        public SubjectController(ISubjectService subjectService) : base(subjectService)
        { }
        [HttpGet("exam-types")]
        public async Task<IActionResult> GetExamTypes()
        {
            var result = await _service.GetExamTypes();
            return ResponseModel(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetSubjects()
        {
            var result = await _service.GetAllSubjects();
            return ResponseModel(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AddSubject([FromBody] SubjectDto subjectDto)
        {
            var result = await _service.CreateSubject(subjectDto);
            return ResponseModel(result);
        }

        [HttpPost("plan")]
        public async Task<IActionResult> AddSubjectPlan([FromBody] SubjectPlan plan)
        {
            var result = await _service.CreateSubjectPlan(plan);
            return ResponseModel(result);
        }

        // [HttpPost("work")]
        // public async Task<IActionResult> AddWork([FromBody] WorkDto work)
        // {
        //     return Ok();
        // }
    }
}
