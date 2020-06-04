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
        Task<List<SubjectView>> GetAllSubjects(int skip);
        Task<List<MainSubjectView>> GetDistinctSubjects(int semesterId);
        Task<List<ExamType>> GetExamTypes();
        Task<SubjectDto> GetSubjectById(int subjectId);
        Task<bool> CreateSubject(SubjectDto subjectDto);
        Task<bool> UpdateSubject(int subjectId, SubjectDto subject);
        Task<bool> DeleteById(int subjectId);
        Task<List<WorkView>> GetWorks(int subjectId);
        Task<List<StudentView>> GetStudents(int subjectId);
        Task<List<StudentView>> GetStudentsWithoutExam(int subjectId);
        Task<List<SubjectView>> GetSubjectsWithWorks(int semesterId);
        Task<List<SubjectView>> GetSubjectsByName(string name, int semesterId);
        Task<List<Status>> GetEmployeeRoles(int subjectId, int employeeId);
        Task<List<Total>> GetStudentSubjectTotals(int subjectId, List<int> studentIds);
        Task<List<Total>> GetStudentAdditionalTotals(int subjectId, List<int> studentIds);
        Task<List<ExamResultDto>> GetExamResults(int subjectId, List<int> studentIds);
        Task<bool> UpdateExamResults(List<ExamResultDto> examResults);
    }
}
