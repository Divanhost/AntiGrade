using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntiGrade.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseServiceController<ILoginService>
    {
        public LoginController(ILoginService tokenService) : base(tokenService)
        {}
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _service.LoginAsync(model);
            return Json(result);
        }

        [AllowAnonymous]
        [HttpPost("renew")]
        public async Task<IActionResult> Renew([FromBody] TokenCouple model)
        {
            var result = await _service.RenewAsync(model);
            return Json(result);
        }
    }
}
