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

        public async Task<Subject> CreateSubject(SubjectDto subjectDto)
        {
            var subject = new Subject()
            {
                Name = subjectDto.Name,
                Teachers = new List<Employee>()
            };
            var type = await _unitOfWork.GetRepository<ExamType, int>().Find(x => x.Id == subjectDto.ExamTypeId);
            subject.Type = type;
            foreach (var item in subjectDto.Teachers)
            {
                var employee = await _unitOfWork.GetRepository<Employee, int>().Filter(x => x.Id == item.Id).FirstOrDefaultAsync();
                subject.Teachers.Add(employee);
            }
            var result = _unitOfWork.GetRepository<Subject, int>().Create(subject);
            await _unitOfWork.Save();
            return result;
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

        public async Task<SubjectView> GetSubjectById(int subjectId)
        {
            var subject = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => x.Id == subjectId)
                                    .ProjectTo<SubjectView>()
                                    .FirstOrDefaultAsync();
            return subject;
        }

        public async Task<Subject> UpdateSubject(int subjectId, SubjectDto subjectDto)
        {
            if (subjectDto != null)
            {
                var subject = await _unitOfWork.GetRepository<Subject, int>()
                   .Filter(x => x.Id == subjectId)
                   .FirstOrDefaultAsync();
                if (subject != null)
                {
                    _mapper.Map(subjectDto, subject);
                    _unitOfWork.GetRepository<Subject, int>()
                        .Update(subject);
                    await _unitOfWork.Save();
                }
                else
                {
                    throw new WebsiteException("Дисциплина не существуетs");
                }
                return subject;
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
                if (!subject.HasPlan)
                {
                    subject.HasPlan = true;
                }
            }
            return await _unitOfWork.Save() > 0;
        }
        public async Task<bool> UpdateSubjectPlan(SubjectPlan plan)
        {
            var worksOld = await _unitOfWork.GetRepository<Work, int>().Filter(x => x.SubjectId == plan.SubjectId).Include(x=>x.Criterias).ToListAsync();
            var worksNew = _mapper.Map<List<Work>>(plan.Works);

            var oldCriterias = worksOld.SelectMany(x=>x.Criterias).ToList();
            var newCriterias = worksNew.SelectMany(x=>x.Criterias).ToList();
            
            worksNew.ForEach(x=> {
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
                                    .ProjectTo<WorkView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return result;
        }
        public async Task<List<StudentView>> GetStudents(int subjectId)
        {
            var result = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => x.Id == subjectId)
                                    .SelectMany(x => x.SubjectGroups)
                                    .Select(x => x.Group)
                                    .SelectMany(y => y.Students)
                                    .ProjectTo<StudentView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return result;
        }

        public async Task<List<GroupView>> UpdateSubjectGroups(int subjectId, List<SubjectGroup> subjectGroups)
        {
            var subjectToUpdate = await _unitOfWork.GetRepository<Subject, int>()
                                                    .Filter(x => x.Id == subjectId)
                                                    .Include(x=>x.SubjectGroups)
                                                    .FirstOrDefaultAsync();
            _unitOfWork.GetRepository<SubjectGroup, int>().Update(subjectToUpdate.SubjectGroups, subjectGroups);
            await _unitOfWork.Save();
            var groups = await GetGroups(subjectId);
            return groups;
        }
        public async Task<List<GroupView>> GetGroups(int subjectId){
            return await _unitOfWork.GetRepository<SubjectGroup, int>()
                                                    .Filter(x => x.SubjectId == subjectId)
                                                    .Select(x=>x.Group)
                                                    .ProjectTo<GroupView>(_mapper.ConfigurationProvider)
                                                    .ToListAsync();
        }
    }
}
