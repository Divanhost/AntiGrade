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
        Task<Subject> UpdateSubject(int subjectId, SubjectDto subject);
        Task<bool> DeleteById(int subjectId);
    }
}
