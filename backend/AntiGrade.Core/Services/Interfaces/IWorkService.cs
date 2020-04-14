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
        Task<List<StudentCriteria>> GetStudentCriteria(List<int> studentIds);
        Task<bool> UpdateStudentCriteria(List<StudentCriteria> studentCriteria);

        Task<List<StudentWork>> GetStudentWorks(int subjectId, List<int> workIds);
        Task<bool> UpdateStudentWorks(List<StudentWork> studentWorks);
    }
}
