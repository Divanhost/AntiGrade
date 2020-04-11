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
    public class SubjectService : ServiceBase, ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<bool> CreateSubject(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            var type = await _unitOfWork.GetRepository<ExamType, int>().Find(x => x.Id == subjectDto.ExamType.Id);
            subject.TypeId = type.Id;
            var result = _unitOfWork.GetRepository<Subject, int>().Create(subject);
            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteById(int subjectId)
        {
            var subject = await _unitOfWork.GetRepository<Subject, int>()
                .Filter(x => x.Id == subjectId)
                .FirstOrDefaultAsync();
            if (subject != null)
            {
                _unitOfWork.GetRepository<Subject, int>()
                    .Delete(subject);
                bool result = await _unitOfWork.Save() > 0;
                return result;
            }
            else
            {
                throw new WebsiteException("Такой дисциплины не существует");
            }
        }

        public async Task<List<SubjectView>> GetAllSubjects()
        {
            var subjects = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted)
                                    .ProjectTo<SubjectView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return subjects;
        }

        public async Task<SubjectDto> GetSubjectById(int subjectId)
        {
            var subject = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => x.Id == subjectId)
                                    .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync();
            return subject;
        }

        public async Task<bool> UpdateSubject(int subjectId, SubjectDto subjectDto)
        {
            if (subjectDto != null)
            {
                var subject = await _unitOfWork.GetRepository<Subject, int>()
                   .Filter(x => x.Id == subjectId)
                   .Include(x => x.SubjectEmployees)
                   .FirstOrDefaultAsync();
                subject = _mapper.Map<Subject>(subjectDto);
                var group = await _unitOfWork.GetRepository<Group, int>()
                   .Filter(x => x.Id == subjectDto.Group.Id)
                   .FirstOrDefaultAsync();
                if (subject != null)
                {
                    subject.Name = subjectDto.Name;
                    subject.Type = subjectDto.ExamType;
                    subject.Group = group;
                    // subject.Works = subjectDto.Works
                    var employeesNew = _mapper.Map<List<SubjectEmployee>>(subjectDto.SubjectEmployees);
                    var employeesOld = subject.SubjectEmployees;

                    subject.SubjectEmployees = null;
                    _unitOfWork.GetRepository<Subject, int>().Update(subject);
                    _unitOfWork.GetRepository<SubjectEmployee, int>().Update(employeesOld, employeesNew);
                    return await _unitOfWork.Save() > 0;
                }
                else
                {
                    throw new WebsiteException("Дисциплина не существуетs");
                }
            }
            else
            {
                throw new WebsiteException("Дисциплина не существует");
            }
        }
        public async Task<List<ExamType>> GetExamTypes()
        {
            var examTypes = await _unitOfWork.GetRepository<ExamType, int>().All().ToListAsync();
            return examTypes;
        }

        public async Task<bool> CreateSubjectPlan(SubjectPlan plan)
        {
            var subject = await _unitOfWork.GetRepository<Subject, int>().Find(x => x.Id == plan.SubjectId);
            if (subject == null)
            {
                throw new WebsiteException("Такого предмета не существует");
            }
            foreach (var workDto in plan.Works)
            {
                if (workDto.Name == null)
                {
                    continue;
                }
                var work = new Work
                {
                    Name = workDto.Name,
                    MaxPoints = workDto.Points,
                    SubjectId = plan.SubjectId
                };
                var criterias = _mapper.Map<List<CriteriaDto>, List<Criteria>>(workDto.Criterias);

                work.Criterias = criterias;
                _unitOfWork.GetRepository<Work, int>().Create(work);
            }
            return await _unitOfWork.Save() > 0;
        }
        public async Task<bool> UpdateSubjectPlan(SubjectPlan plan)
        {
            var worksOld = await _unitOfWork.GetRepository<Work, int>().Filter(x => x.SubjectId == plan.SubjectId).Include(x => x.Criterias).ToListAsync();
            var worksNew = _mapper.Map<List<Work>>(plan.Works);

            var oldCriterias = worksOld.SelectMany(x => x.Criterias).ToList();
            var newCriterias = worksNew.SelectMany(x => x.Criterias).ToList();

            worksNew.ForEach(x =>
            {
                x.SubjectId = plan.SubjectId;
                x.Criterias = null;
            });
            _unitOfWork.GetRepository<Work, int>().Update(worksOld, worksNew);
            _unitOfWork.GetRepository<Criteria, int>().Update(oldCriterias, newCriterias);
            return await _unitOfWork.Save() > 0;
        }

        public async Task<List<WorkView>> GetWorks(int subjectId)
        {
            var result = await _unitOfWork.GetRepository<Work, int>()
                                    .Filter(x => x.SubjectId == subjectId)
                                    .OrderBy(x => x.WorkType.Id)
                                    .ProjectTo<WorkView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return result;
        }
        public async Task<List<StudentView>> GetStudents(int subjectId)
        {
            var result = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => x.Id == subjectId)
                                    .Select(x => x.Group)
                                    .SelectMany(y => y.Students)
                                    .ProjectTo<StudentView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return result;
        }

        public async Task<List<MainSubjectView>> GetDistinctSubjects()
        {
            var subjects = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted)
                                    .GroupBy(x => x.Name)
                                    .Select(y => y.First())
                                    .OrderBy(y => y.Name)
                                    .ProjectTo<MainSubjectView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return subjects;
        }

        public async Task<List<SubjectView>> GetSubjectsWithWorks()
        {
            var subjects = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted && x.Works.Any())
                                    .ProjectTo<SubjectView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return subjects;
        }

        // public async Task<List<GroupView>> UpdateSubjectGroups(int subjectId, List<SubjectGroup> subjectGroups)
        // {
        //     var subjectToUpdate = await _unitOfWork.GetRepository<Subject, int>()
        //                                             .Filter(x => x.Id == subjectId)
        //                                             .Include(x=>x.SubjectGroups)
        //                                             .FirstOrDefaultAsync();
        //     _unitOfWork.GetRepository<SubjectGroup, int>().Update(subjectToUpdate.SubjectGroups, subjectGroups);
        //     await _unitOfWork.Save();
        //     var groups = await GetGroups(subjectId);
        //     return groups;
        // }
        // public async Task<List<GroupView>> GetGroups(int subjectId){
        //     return await _unitOfWork.GetRepository<SubjectGroup, int>()
        //                                             .Filter(x => x.SubjectId == subjectId)
        //                                             .Select(x=>x.Group)
        //                                             .Where(g=>!g.IsDeleted)
        //                                             .ProjectTo<GroupView>(_mapper.ConfigurationProvider)
        //                                             .ToListAsync();
        // }
    }
}
