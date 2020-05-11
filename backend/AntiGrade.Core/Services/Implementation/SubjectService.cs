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
            subject.SubjectEmployees = DivideSubjectEmployees(subjectDto.SubjectEmployees); 
            // subject.Works.ForEach(x=>x.Criterias.ForEach(y=>y.WorkId = x.Id));
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
            var subjectEmployees = await _unitOfWork.GetRepository<SubjectEmployee, int>()
                                    .Filter(x => x.SubjectId == subjectId)
                                    .Include(y=>y.Status)
                                    .ToListAsync();
            subject.SubjectEmployees = UniteSubjectEmployees(subjectEmployees);
            return subject;
        }

        public async Task<bool> UpdateSubject(int subjectId, SubjectDto subjectDto)
        {
            if (subjectDto != null)
            {
                var subject = await _unitOfWork.GetRepository<Subject, int>()
                   .Filter(x => x.Id == subjectId)
                   .Include(x => x.SubjectEmployees)
                   .Include(x => x.Works)
                   .FirstOrDefaultAsync();
                var group = await _unitOfWork.GetRepository<Group, int>()
                   .Filter(x => x.Id == subjectDto.Group.Id)
                   .FirstOrDefaultAsync();
                if (subject != null)
                {
                    subject.Name = subjectDto.Name;
                    subject.TypeId = subjectDto.ExamType.Id;
                    subject.Group = group;
                    var works = _mapper.Map<List<Work>>(subjectDto.Works);
                    // works.ForEach(x=>x.Criterias.ForEach(y=>y.WorkId = x.Id));
                    var employeesNew = DivideSubjectEmployees(subjectDto.SubjectEmployees);
                    
                    _unitOfWork.GetRepository<Subject, int>().Update(subject);

                    _unitOfWork.GetRepository<SubjectEmployee, int>().Update(subject.SubjectEmployees, employeesNew);

                    await UpdateWorks(works,subjectId);

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

        private async Task UpdateWorks(List<Work> works,int subjectId) {
            var worksOld = await _unitOfWork.GetRepository<Work, int>().Filter(x => x.SubjectId == subjectId).Include(x => x.Criterias).ToListAsync();
            var worksNew = works;

            var oldCriterias = worksOld.SelectMany(x => x.Criterias).ToList();
            var newCriterias = worksNew.SelectMany(x => x.Criterias).ToList();

            worksNew.ForEach(x =>
            {
                x.SubjectId = subjectId;
                x.Criterias = null;
            });
            _unitOfWork.GetRepository<Work, int>().Update(worksOld, worksNew);
            _unitOfWork.GetRepository<Criteria, int>().Update(oldCriterias, newCriterias);
        }
        private async Task CreateWorks(List<Work> works) {
            _unitOfWork.GetRepository<Work, int>().Create(works);
            await _unitOfWork.Save();
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
                                    .OrderBy(z=>z.LastName)
                                    .ThenBy(z=>z.FirstName)
                                    .ThenBy(x=>x.Patronymic)
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

        public async Task<List<SubjectView>> GetSubjectsByName(string name)
        {
            var subjects = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted && x.Name == name)
                                    .OrderBy(x=>x.Group.Name)
                                    .ProjectTo<SubjectView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return subjects;
        }

        public async Task<List<SubjectView>> GetSubjectsWithWorks()
        {
            var subjects = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted && x.Works.Any())
                                    .OrderBy(x=>x.Name)
                                    .ProjectTo<SubjectView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return subjects;
        }

        public async Task<List<Status>> GetEmployeeRoles(int subjectId, int employeeId) {
            var roles = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted && x.Id == subjectId)
                                    .SelectMany(x => x.SubjectEmployees)
                                    .Where(y => y.EmployeeId == employeeId)
                                    .Select(z=>z.Status)
                                    .ToListAsync();
            return roles;
        }
        public async Task<List<ExamResultDto>> GetExamResults(int subjectId, List<int> studentIds) {
            var result = await _unitOfWork.GetRepository<ExamResult, int>()
                                    .Filter(x => x.SubjectId == subjectId && studentIds.Contains(x.StudentId))
                                    .OrderBy(x=>x.Student.LastName)
                                    .ThenBy(x=>x.Student.FirstName)
                                    .ThenBy(x=>x.Student.Patronymic)
                                    .ProjectTo<ExamResultDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return result;
        }
        public async Task<bool> UpdateExamResults(List<ExamResultDto> examResults) {
            var examResultsForUpdate = examResults.Where(x => x.Id != 0).ToList();
            var examResultsForCreate = examResults.Where(x => x.Id == 0).ToList();


            var dbExamResultsCreate = _mapper.Map<List<ExamResult>>(examResultsForCreate);
            var dbExamResultsUpdate = _mapper.Map<List<ExamResult>>(examResultsForUpdate);

            _unitOfWork.GetRepository<ExamResult, int>().Create(dbExamResultsCreate);

            _unitOfWork.GetRepository<ExamResult, int>().Update(dbExamResultsUpdate);

           await _unitOfWork.Save();
           return true;
        }
         public async Task<List<Total>> GetStudentSubjectTotals(int subjectId, List<int> studentIds) {
            var totals = await GetTotals(subjectId, studentIds, false);
            return totals;
        }

        public async Task<List<Total>> GetStudentAdditionalTotals(int subjectId, List<int> studentIds) {
            var totals = await GetTotals(subjectId, studentIds, true);
            return totals;
        }
        private async Task<List<Total>> GetTotals(int subjectId, List<int> studentIds, bool isAdditional) {
            var workIds = await _unitOfWork.GetRepository<Subject, int>()
                                    .Filter(x => !x.IsDeleted && x.Id == subjectId)
                                    .SelectMany(x => x.Works)
                                    .Select(y=>y.Id)
                                    .ToListAsync();
            var studentWorks = await _unitOfWork.GetRepository<StudentWork, int>()
                                    .Filter(x=> studentIds.Contains(x.StudentId) && workIds.Contains(x.WorkId) && x.IsAdditional == isAdditional)
                                    .ToListAsync();
            var totals = new List<Total>();
            foreach (var id in studentIds)
            {
                var studentTotal = studentWorks.Where(x=>x.StudentId == id).Sum(x=>x.SumOfPoints);
                var total = new Total()
                {
                    StudentId = id,
                    Totals = studentTotal
                };
                totals.Add(total);
            }
            return totals;
        }
        private List<SubjectEmployee> DivideSubjectEmployees(List<SubjectEmployeeDto> subjectEmployeeDto) {
            var result = new List<SubjectEmployee>();
            foreach (var item in subjectEmployeeDto)
            {
                foreach (var status in item.Statuses)
                {   
                    var se = new SubjectEmployee()
                    {
                        Id = item.Id,
                        SubjectId = item.SubjectId,
                        EmployeeId = item.EmployeeId,
                        StatusId = status.Id
                    };
                    result.Add(se);
                }
            }
            return result;
        }
        private List<SubjectEmployeeDto> UniteSubjectEmployees(List<SubjectEmployee> subjectEmployeeDto) {
            var result = subjectEmployeeDto.GroupBy(x=> new {x.SubjectId, x.EmployeeId})
                                            .Select(group => new SubjectEmployeeDto
                                                        {
                                                            SubjectId = group.Key.SubjectId,
                                                            EmployeeId = group.Key.EmployeeId,
                                                            Statuses = group.ToList().Select(y=>y.Status).ToList()
                                                        })
                                            .ToList();
            return result;
        }
    }
}
