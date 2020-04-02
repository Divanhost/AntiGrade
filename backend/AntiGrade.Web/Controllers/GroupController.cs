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

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] GroupDto groupDto)
        {
            var result = await _service.CreateGroup(groupDto);
            return ResponseModel(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var result = await _service.GetAllGroups();
            return ResponseModel(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var result = await _service.GetGroupById(id);
            return ResponseModel(result);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupDto data)
        {
            var result = await _service.UpdateGroup(id, data);
            return ResponseModel(result);
        }
        [HttpGet("subject/{id}")]
        public async Task<IActionResult> GetGroupBySubjectId(int id)
        {
            var result = await _service.GetGroupBySubjectId(id);
            return ResponseModel(result);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetGroupsBySubjectName(string name)
        {
            var result = await _service.GetGroupsBySubjectName(name);
            return ResponseModel(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var result = await _service.DeleteById(id);
            return ResponseModel(result);
        }
    }
}
