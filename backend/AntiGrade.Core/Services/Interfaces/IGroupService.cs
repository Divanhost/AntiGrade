using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<GroupView>> GetAllGroups();
        Task<GroupView> GetGroupById(int id);
        Task<List<GroupView>> GetGroupBySubjectId(int id);
        Task<bool> CreateGroup(GroupDto groupDto);
        Task<bool> UpdateGroup(int groupId, GroupDto groupDto);
        Task<bool> DeleteById(int id);
    }
}
