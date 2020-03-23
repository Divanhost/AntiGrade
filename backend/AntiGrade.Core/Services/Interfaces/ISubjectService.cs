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
        Task<List<ExamType>> GetExamTypes();
        Task<SubjectView> GetSubjectById(int subjectId);
        Task<Subject> CreateSubject(SubjectDto subjectDto);
        Task<bool> CreateSubjectPlan(SubjectPlan plan);
        Task<Subject> UpdateSubject(int subjectId, SubjectDto subject);
        Task<List<GroupView>> UpdateSubjectGroups(int subjectId, List<GroupDto> subject);
        Task<bool> DeleteById(int subjectId);
        Task<List<WorkView>> GetWorks(int subjectId);
        Task<List<StudentView>> GetStudents(int subjectId);
    }
}