using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectView>> GetAllSubjects();
        Task<List<MainSubjectView>> GetDistinctSubjects();
        Task<List<ExamType>> GetExamTypes();
        Task<SubjectDto> GetSubjectById(int subjectId);
        Task<bool> CreateSubject(SubjectDto subjectDto);
        Task<bool> UpdateSubject(int subjectId, SubjectDto subject);
        Task<bool> DeleteById(int subjectId);
        Task<List<WorkView>> GetWorks(int subjectId);
        Task<List<StudentView>> GetStudents(int subjectId);
        Task<List<SubjectView>> GetSubjectsWithWorks();
        Task<List<string>> GetEmployeeRoles(int subjectId, int employeeId);
        Task<List<Total>> GetStudentSubjectTotals(int subjectId, List<int> studentIds);
        Task<List<ExamResultDto>> GetExamResults(int subjectId, List<int> studentIds);
        Task<bool> UpdateExamResults(List<ExamResultDto> examResults);
    }
}
