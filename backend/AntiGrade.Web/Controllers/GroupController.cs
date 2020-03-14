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
    public class GroupController : BaseServiceController<IGroupService>
    {
        public GroupController(IGroupService groupService) : base(groupService)
        { }

        [HttpPost()]
        public async Task<IActionResult> AddGroup([FromBody] GroupDto groupDto)
        {
            var result = await _service.CreateGroup(groupDto);
            return ResponseModel(result);
        }
    }
}
