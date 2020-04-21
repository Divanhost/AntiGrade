using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IWorkService
    {
        Task<List<StudentCriteriaDto>> GetStudentCriteria(int workId);
        Task<List<StudentCriteriaDto>> GetAdditionalStudentCriteria(int workId);
        Task<bool> UpdateStudentCriteria(List<StudentCriteriaDto> studentCriteria);

        Task<List<StudentWorkDto>> GetStudentWorks(int subjectId);
        Task<List<StudentWorkDto>> GetAdditionalStudentWorks(int subjectId);
        Task<bool> UpdateStudentWorks(List<StudentWork> studentWorks);
    }
}
