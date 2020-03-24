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
        Task<SubjectDto> GetSubjectById(int subjectId);
        Task<bool> CreateSubject(SubjectDto subjectDto);
        Task<bool> CreateSubjectPlan(SubjectPlan plan);
        Task<bool> UpdateSubjectPlan(SubjectPlan plan);
        Task<bool> UpdateSubject(int subjectId, SubjectDto subject);
        Task<List<GroupView>> UpdateSubjectGroups(int subjectId, List<SubjectGroup> subjectGroups);
        Task<bool> DeleteById(int subjectId);
        Task<List<WorkView>> GetWorks(int subjectId);
        Task<List<StudentView>> GetStudents(int subjectId);
    }
}
