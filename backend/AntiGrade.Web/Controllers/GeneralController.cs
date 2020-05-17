using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;
using AntiGrade.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntiGrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class GeneralController : BaseServiceController<IGeneralService>
    {
        public GeneralController(IGeneralService generalService) : base(generalService)
        { }

        [HttpGet("mode")]
        public async Task<IActionResult> GetMode()
        {
            var result = await _service.GetCurrentMode();
            return ResponseModel(result);
        }
        [HttpGet("mode/{id:int}")]
        public async Task<IActionResult> UpdateMode(int id)
        {
            var result = await _service.UpdateCurrentMode(id);
            return ResponseModel(result);
        }
        [HttpGet("statuses")]
        public async Task<IActionResult> GetStatuses()
        {
            var result = await _service.GetAllStatuses();
            return ResponseModel(result);
        }
        #region InstituteCRUD

        [HttpGet("institutes")]
        public async Task<IActionResult> GetAllInsitutes()
        {
            var result = await _service.GetInstitutes();
            return ResponseModel(result);
        }
        [HttpGet("institutes/{id}")]
        public async Task<IActionResult> GetAllInsitutes(int id)
        {
            var result = await _service.GetInstitute(id);
            return ResponseModel(result);
        }
        [HttpPost("institutes")]
        public async Task<IActionResult> CreateInstitute([FromBody] InstituteView institute)
        {
            var result = await _service.CreateInstitute(institute);
            return ResponseModel(result);
        }
        [HttpPut("institutes")]
        public async Task<IActionResult> UpdateInstitute([FromBody] InstituteView institute)
        {
            var result = await _service.UpdateInstitute(institute);
            return ResponseModel(result);
        }
        [HttpDelete("institutes")]
        public async Task<IActionResult> DeleteInstitute([FromQuery] int id)
        {
            var result = await _service.DeleteInstitute(id);
            return ResponseModel(result);
        }
        #endregion

        #region DepartmentCRUD

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments([FromQuery] int id)
        {
            var result = await _service.GetDepartments(id);
            return ResponseModel(result);
        }
        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment([FromBody] List<DepartmentView> departments)
        {
            var result = await _service.CreateDepartments(departments);
            return ResponseModel(result);
        }
        // [HttpPut("departments")]
        // public async Task<IActionResult> UpdateDepartment([FromBody] List<DepartmentView> departments)
        // {
        //     var result = await _service.UpdateDepartments(departments);
        //     return ResponseModel(result);
        // }
        [HttpDelete("departments")]
        public async Task<IActionResult> DeleteDepartment([FromQuery] int id)
        {
            var result = await _service.DeleteDepartment(id);
            return ResponseModel(result);
        }
        #endregion

        #region CourseCRUD

        [HttpGet("courses/all")]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _service.GetCourses();
            return ResponseModel(result);
        }
         [HttpGet("courses/{id}")]
        public async Task<IActionResult> GetCourses(int id)
        {
            var result = await _service.GetCourses();
            return ResponseModel(result);
        }
        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseView course)
        {
            var result = await _service.CreateCourse(course);
            return ResponseModel(result);
        }
        [HttpPut("courses")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseView course)
        {
            var result = await _service.UpdateCourse(course);
            return ResponseModel(result);
        }
        [HttpDelete("courses")]
        public async Task<IActionResult> DeleteCourse([FromQuery] int id)
        {
            var result = await _service.DeleteCourse(id);
            return ResponseModel(result);
        }
        #endregion

        [HttpGet("semesters")]
        public async Task<IActionResult> GetSemesters()
        {
            var result = await _service.GetSemesters();
            return ResponseModel(result);
        }
        [HttpPost("semesters")]
        public async Task<IActionResult> CreateSemester([FromBody] SemesterView semester)
        {
            var result = await _service.CreateNewSemester(semester);
            return ResponseModel(result);
        }
    }
}
