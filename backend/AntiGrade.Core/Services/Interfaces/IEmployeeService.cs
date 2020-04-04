using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeView>> GetAllEmployees();
        Task<List<EmployeeView>> GetAllTeachers();
        Task<List<EmployeeView>> GetEmployeesList(List<int> ids);
        Task<List<SubjectEmployee>> GetSubjectEmployees(int subjectId);
        Task<EmployeeDto> GetEmployeeById(int employeeId);
        Task<bool> CreateEmployee(EmployeeDto employeeDto);
        Task<Employee> UpdateEmployee(int employeeId, EmployeeDto Employee);
        Task<bool> DeleteById(int employeeId);
        Task<List<EmployeePosition>> GetEmployeePositions();
    }
}
