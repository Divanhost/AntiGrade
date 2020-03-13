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
    }
}
