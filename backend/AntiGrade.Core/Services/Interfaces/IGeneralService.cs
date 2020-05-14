using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IGeneralService
    {
        Task<int> GetCurrentMode();
        Task<bool> UpdateCurrentMode(int id);
        Task<List<Status>> GetAllStatuses();
        
        // Institute CRUD
        Task<InstituteView> GetInstitute(int id);
        Task<List<InstituteView>> GetInstitutes();
        Task<bool> CreateInstitute(InstituteView institute);
        Task<bool> UpdateInstitute(InstituteView institute);
        Task<bool> DeleteInstitute(int id);
        // end Institute CRUD

        // Department CRUD
        Task<List<DepartmentView>> GetDepartments(int instituteId);
        Task<bool> CreateDepartments(List<DepartmentView> departments);
        // Task<bool> UpdateDepartments(List<DepartmentView> departments);
        Task<bool> DeleteDepartment(int id);
        // end Department CRUD

         // Course CRUD
        Task<List<CourseView>> GetCourses();
        Task<bool> CreateCourse(CourseView course);
        Task<bool> UpdateCourse(CourseView course);
        Task<bool> DeleteCourse(int id);
        // end Course CRUD
    }
}

        
