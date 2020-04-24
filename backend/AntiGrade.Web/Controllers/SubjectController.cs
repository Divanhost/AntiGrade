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

        [HttpGet("roles")]
        public async Task<IActionResult> GetEmployeeRole([FromQuery] int employeeId,[FromQuery] int subjectId)
        {
            var result = await _service.GetEmployeeRoles(subjectId,employeeId);
            return ResponseModel(result);
        }

        [HttpGet("total/{id:int}")]
        public async Task<IActionResult> GetStudentTotals(int id,[FromQuery] List<int> studentIds)
        {
            var result = await _service.GetStudentSubjectTotals(id, studentIds);
            return ResponseModel(result);
        }
        [HttpGet("total/additional/{id:int}")]
        public async Task<IActionResult> GetStudentAdditionalTotals(int id,[FromQuery] List<int> studentIds)
        {
            var result = await _service.GetStudentAdditionalTotals(id, studentIds);
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
        [HttpGet("exam/{id:int}")]
        public async Task<IActionResult> GetExamResults(int id, [FromQuery] List<int> studentIds)
        {
            var result = await _service.GetExamResults(id, studentIds);
            return ResponseModel(result);
        }
        [HttpPut("exam")]
        public async Task<IActionResult> UpdateExamResults([FromBody] List<ExamResultDto> examResults)
        {
            var result = await _service.UpdateExamResults(examResults);
            return ResponseModel(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveSubject(int id)
        {
            var result = await _service.DeleteById(id);
            return ResponseModel(result);
        }
    }
}
