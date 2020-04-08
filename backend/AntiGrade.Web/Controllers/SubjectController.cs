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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var result = await _service.GetSubjectById(id);
            return ResponseModel(result);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDto subject)
        {
            var result = await _service.UpdateSubject(id,subject);
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
        [HttpPut("plan")]
        public async Task<IActionResult> UpdateSubjectPlan([FromBody] SubjectPlan plan)
        {
            var result = await _service.UpdateSubjectPlan(plan);
            return ResponseModel(result);
        }

        [HttpGet("works/{id:int}")]
        public async Task<IActionResult> GetSubjectWorks(int id)
        {
            var result = await _service.GetWorks(id);
            return ResponseModel(result);
        }

        [HttpGet("students/{id:int}")]
        public async Task<IActionResult> GetSubjectStudents(int id)
        {
            var result = await _service.GetStudents(id);
            return ResponseModel(result);
        }
        [HttpGet("distinct")]
        public async Task<IActionResult> GetDistinctSubjects()
        {
            var result = await _service.GetDistinctSubjects();
            return ResponseModel(result);
        }
        [HttpGet("filled")]
        public async Task<IActionResult> GetSubjectWithWorks()
        {
            var result = await _service.GetSubjectsWithWorks();
            return ResponseModel(result);
        }
        // [HttpPut("groups/{id:int}")]
        // public async Task<IActionResult> UpdateSubjectGroups(int id,[FromBody] List<SubjectGroup> groups)
        // {
        //     var result = await _service.UpdateSubjectGroups(id, groups);
        //     return ResponseModel(result);
        // }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveSubject(int id)
        {
            var result = await _service.DeleteById(id);
            return ResponseModel(result);
        }
    }
}
