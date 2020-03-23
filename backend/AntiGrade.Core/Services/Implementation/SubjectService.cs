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
            var subject =new Subject()
            {   
                Name = subjectDto.Name,
                Teachers = new List<Employee>()
            };
            var type = await _unitOfWork.GetRepository<ExamType,int>().Find(x=>x.Id == subjectDto.ExamTypeId);
            subject.Type = type;
            foreach (var item in subjectDto.Teachers)
            {
                var employee = await _unitOfWork.GetRepository<Employee,int>().Filter(x=>x.Id == item.Id).FirstOrDefaultAsync();
                subject.Teachers.Add(employee);
            }
            var result = _unitOfWork.GetRepository<Subject,int>().Create(subject);
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
            var subjects = await _unitOfWork.GetRepository<Subject,int>()
                                    .Filter(x=>!x.IsDeleted)
                                    .ProjectTo<SubjectView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return subjects;
        }

        public async Task<SubjectView> GetSubjectById(int subjectId)
        {
            var subject = await _unitOfWork.GetRepository<Subject,int>()
                                    .Filter(x=>x.Id == subjectId)
                                    .ProjectTo<SubjectView>()
                                    .FirstOrDefaultAsync();
            return subject;
        }

        public async Task<Subject> UpdateSubject(int subjectId, SubjectDto subjectDto)
        {
            if(subjectDto != null)
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
            var examTypes = await _unitOfWork.GetRepository<ExamType,int>().All().ToListAsync();
            return examTypes;
        }

        public async Task<bool> CreateSubjectPlan(SubjectPlan plan)
        {
            var subject = await _unitOfWork.GetRepository<Subject,int>().Find(x=>x.Id == plan.SubjectId);
            if(subject == null)
            {
                throw new WebsiteException("Такого предмета не существует");
            }
            foreach (var workDto in plan.Works)
            {
                if(workDto.Name == null){
                    continue;
                }
                 var work = new Work
                {
                    Name = workDto.Name,
                    MaxPoints =workDto.MaxPoints,
                    SubjectId = plan.SubjectId
                };
                // var criterias = new List<Criteria>();

                // foreach (var criteriaDto in workDto.Criterias)
                // {
                //     var criteria = _mapper.Map<Criteria>(criteriaDto);
                //     criterias.Add(criteria);
                // }
                var criterias = _mapper.Map<List<CriteriaDto>,List<Criteria>>(workDto.Criterias);
                if(criterias.Count == 0){
                    criterias.Add(new Criteria{
                        Name = "Критерий 1",
                        MaxPoints = workDto.MaxPoints,
                    });
                }
                work.Criterias = criterias;
                _unitOfWork.GetRepository<Work,int>().Create(work);
                if(!subject.HasPlan){
                    subject.HasPlan = true;
                }
            }   
            return await _unitOfWork.Save() > 0;
        }

        public async Task<List<WorkView>> GetWorks(int subjectId)
        {
            var result = await _unitOfWork.GetRepository<Work,int>()
                                    .Filter(x=>x.SubjectId == subjectId)
                                    .ProjectTo<WorkView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return result;
        }
        public async Task<List<StudentView>> GetStudents(int subjectId)
        {
            // var result = await _unitOfWork.GetRepository<Subject,int>()
            //                         .Filter(x=>x.Id == subjectId)
            //                         .SelectMany(x => x.Groups)
            //                         .SelectMany(y=>y.Students)
            //                         .ProjectTo<StudentView>(_mapper.ConfigurationProvider)
            //                         .ToListAsync();
            return null;
        }

        public async Task<List<GroupView>> UpdateSubjectGroups(int subjectId, List<GroupDto> groupsDto)
        {
            var subjectToUpdate = await  _unitOfWork.GetRepository<Subject,int>().Filter(x=>x.Id == subjectId).FirstOrDefaultAsync();
            var groups = _mapper.Map<List<Group>>(groupsDto);
             _unitOfWork.GetRepository<Subject,int>().Update(subjectToUpdate);
            await _unitOfWork.Save();
            return _mapper.Map<List<GroupView>>(groups);
        }
    }
}
