using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared.Exceptions;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AntiGrade.Core.Services.Implementation
{
    public class GroupService : ServiceBase, IGroupService
    {
        public GroupService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<Group> CreateGroup(GroupDto GroupDto)
        {
            var group =_mapper.Map<Group>(GroupDto);
            var result = _unitOfWork.GetRepository<Group,int>().Create(group);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> DeleteById(int groupId)
        {
            var group = await _unitOfWork.GetRepository<Group, int>()
                .Filter(x => x.Id == groupId)
                .FirstOrDefaultAsync();
            if (group != null)
            {
                _unitOfWork.GetRepository<Group, int>()
                    .Delete(group);
                bool result = await _unitOfWork.Save() > 0;
                return result;
            }
            else
            {
                throw new WebsiteException("Такой дисциплины не существует");
            }
        }

        public async Task<List<GroupView>> GetAllGroups()
        {
            var groups = await _unitOfWork.GetRepository<Group,int>()
                                    .Filter(x=>!x.IsDeleted)
                                    .ProjectTo<GroupView>()
                                    .ToListAsync();
            return groups;
        }

        public async Task<GroupView> GetGroupById(int groupId)
        {
            var group = await _unitOfWork.GetRepository<Group,int>()
                                    .Filter(x=>x.Id == groupId)
                                    .ProjectTo<GroupView>()
                                    .FirstOrDefaultAsync();
            return group;
        }

        public async Task<Group> UpdateGroup(int GroupId, GroupDto groupDto)
        {
            if(groupDto != null)
            {
                 var group = await _unitOfWork.GetRepository<Group, int>()
                    .Filter(x => x.Id == GroupId)
                    .FirstOrDefaultAsync();
                if (group != null)
                {
                    _mapper.Map(groupDto, group);
                    _unitOfWork.GetRepository<Group, int>()
                        .Update(group);
                    await _unitOfWork.Save();
                }
                else
                {
                    throw new WebsiteException("Дисциплина не существуетs");
                }
                return group;
            } 
            else
            {
                throw new WebsiteException("Дисциплина не существует");
            }
        }
    }
}
