using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentView>> GetAllStudents();
        Task<StudentView> GetStudentById(int studentId);
        Task<Student> CreateStudent(StudentDto studentDto);
        Task<Student> UpdateStudent(int studentId, StudentDto student);
        Task<List<StudentCriteria>> GetStudentCriteria(List<int> studentIds);
        Task<bool> DeleteById(int studentId);
    }
}
