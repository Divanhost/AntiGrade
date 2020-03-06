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
        IRepository<SubjectDistribution, int> SubjectDistributionRepository
        {
            get;
        }
        IRepository<EmployeePosition, int> EmployeePositionRepository
        {
            get;
        }
        Task<int> Save();
    }
}