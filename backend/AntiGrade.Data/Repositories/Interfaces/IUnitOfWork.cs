using System.Threading.Tasks;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;

namespace AntiGrade.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class, IEntity<TId>;

        IRepository<User, int> UserRepository
        {
            get;
        }
        IRepository<TokenCouple, int> TokenCoupleRepository
        {
            get;
        }

        IRepository<Subject, int> SubjectRepository
        {
            get;
        }
        IRepository<Employee, int> EmployeeRepository
        {
            get;
        }
        IRepository<Group, int> GroupRepository
        {
            get;
        }
        IRepository<Student, int> StudentRepository
        {
            get;
        }
        IRepository<EmployeePosition, int> EmployeePositionRepository
        {
            get;
        }
        IRepository<Criteria, int> CriteriaRepository
        {
            get;
        }
        IRepository<StudentCriteria, int> StudentCriteriaRepository
        {
            get;
        }
        IRepository<Work, int> WorkRepository
        {
            get;
        }
        IRepository<WorkType, int> WorkTypeRepository
        {
            get;
        }
        IRepository<ExamType, int> ExamTypeRepository
        {
            get;
        }
        IRepository<SubjectEmployee, int> SubjectEmployeeRepository
        {
            get;
        }
        IRepository<ExamResult, int> ExamResultRepository
        {
            get;
        }
        IRepository<Mode, int> ModeRepository
        {
            get;
        }
        IRepository<Status, int> StatusRepository
        {
            get;
        }
        IRepository<Institute, int> InstituteRepository
        {
            get;
        }
        IRepository<Department, int> DepartmentRepository
        {
            get;
        }
        IRepository<Course, int> CourseRepository
        {
            get;
        }
        IRepository<Semester, int> SemesterRepository
        {
            get;
        }
        IRepository<SubjectExamStatus, int> SubjectExamStatusRepository
        {
            get;
        }
        Task<int> Save();
    }
}