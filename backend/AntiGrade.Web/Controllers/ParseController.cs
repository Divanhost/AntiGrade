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
    public class ParseController : BaseServiceController<IExcelService>
    {
        public ParseController(IExcelService excelService) : base(excelService)
        { }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Export(int id)
        {
            var result = await _service.ExportRatingTable(id);
            return ResponseModel(result);
        }
    }
}
