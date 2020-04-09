using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> CreateGroup(GroupDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);

            var result = _unitOfWork.GetRepository<Group, int>().Create(group);
            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteById(int groupId)
        {
            var group = await _unitOfWork.GetRepository<Group, int>()
                .Filter(x => x.Id == groupId)
                .FirstOrDefaultAsync();
            if (group != null)
            {
                group.IsDeleted = true;
                _unitOfWork.GetRepository<Group, int>()
                    .Update(group);
                bool result = await _unitOfWork.Save() > 0;
                return result;
            }
            else
            {
                throw new WebsiteException("Такой группы не существует");
            }
        }

        public async Task<List<GroupView>> GetAllGroups()
        {
            var groups = await _unitOfWork.GetRepository<Group, int>()
                                    .Filter(x => !x.IsDeleted)
                                    .ProjectTo<GroupView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return groups;
        }

        public async Task<GroupView> GetGroupById(int groupId)
        {
            var group = await _unitOfWork.GetRepository<Group, int>()
                                    .Filter(x => x.Id == groupId && !x.IsDeleted)
                                    .ProjectTo<GroupView>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync();
            return group;
        }

        public async Task<List<GroupView>> GetGroupBySubjectId(int id)
        {
            var group = await _unitOfWork.GetRepository<Subject, int>()
                                   .Filter(x => x.Id == id)
                                   .Select(x => x.Group)
                                   .ProjectTo<GroupView>(_mapper.ConfigurationProvider)
                                   .ToListAsync();
            return group;
        }

        public async Task<List<GroupView>> GetGroupsBySubjectName(string name)
        {
            var group = await _unitOfWork.GetRepository<Subject, int>()
                                   .Filter(x => x.Name == name)
                                   .Select(x => x.Group)
                                   .OrderBy(g=>g.Name)
                                   .ProjectTo<GroupView>(_mapper.ConfigurationProvider)
                                   .ToListAsync();
            return group;
        }

        public async Task<bool> UpdateGroup(int GroupId, GroupDto groupDto)
        {
            if (groupDto != null)
            {
                var groupDb = await _unitOfWork.GetRepository<Group, int>()
                   .Filter(x => x.Id == GroupId)
                   .FirstOrDefaultAsync();
                if (groupDb != null)
                {
                    groupDb.Name = groupDto.Name;
                    var newStudents = _mapper.Map<List<Student>>(groupDto.Students);
                     groupDb.Students = null;
                    _unitOfWork.GetRepository<Group, int>()
                        .Update(groupDb);
                    foreach (var item in newStudents)
                    {
                        item.Group = groupDb;
                    }
                    await UpdateStudents(newStudents, groupDb.Id);
                   
                    return await _unitOfWork.Save() > 0;
                }
                else
                {
                    throw new WebsiteException("Группа не существуетs");
                }
            }
            else
            {
                throw new WebsiteException("Группа не существует");
            }
        }
    
        private async Task UpdateStudents(List<Student> newStudents, int groupId)
        {
            var existingIds = await GetStudentIdsForGroup(groupId);
            var studentsForDelete = existingIds
                .Where(sl => !newStudents.Any(c => c.Id == sl)).ToList();
            _unitOfWork.GetRepository<Student, int>().Delete(x => studentsForDelete.Contains(x.Id));
            
            var studentsForUpdate = newStudents.Where(x => x.Id != 0).ToList();
            _unitOfWork.GetRepository<Student, int>().Update(studentsForUpdate);

            var studentsForCreate = newStudents.Where(x => x.Id == 0).ToList();
            _unitOfWork.GetRepository<Student, int>().Create(studentsForCreate);
            //return await _unitOfWork.Save() > 0;
        }

        private async Task<List<int>> GetStudentIdsForGroup(int id)
        {
            var result = await _unitOfWork.GetRepository<Group, int>()
                .Filter(x => x.Id == id)
                .SelectMany(x => x.Students)
                .Select(x=>x.Id)
                .ToListAsync();
            return result;
        }
    }
}
