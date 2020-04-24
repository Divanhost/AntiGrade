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
    }
}
