using System.Collections.Concurrent;
using System.Threading.Tasks;
using AntiGrade.Data.Context;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;

namespace AntiGrade.Data.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IRepository<User, int> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new Repository<User, int>(_context);
                }
                return _userRepository;
            }
        }
        public IRepository<TokenCouple, int> TokenCoupleRepository
        {
            get
            {
                if (_tokenCoupleRepository == null)
                {
                    _tokenCoupleRepository = new Repository<TokenCouple, int>(_context);
                }

                return _tokenCoupleRepository;
            }
        }

        public IRepository<Employee, int> EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new Repository<Employee, int>(_context);
                }

                return _employeeRepository;
            }
        }

        public IRepository<Group, int> GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new Repository<Group, int>(_context);
                }

                return _groupRepository;
            }
        }
          public IRepository<Student, int> StudentRepository
        {
            get
            {
                if (_studentRepository== null)
                {
                    _studentRepository = new Repository<Student, int>(_context);
                }

                return _studentRepository;
            }
        }
        public IRepository<Subject, int> SubjectRepository
        {
            get
            {
                if (_subjectRepository == null)
                {
                    _subjectRepository = new Repository<Subject, int>(_context);
                }

                return _subjectRepository;
            }
        }
        public IRepository<EmployeePosition, int> EmployeePositionRepository
        {
            get
            {
                if (_employeePositionRepository == null)
                {
                    _employeePositionRepository = new Repository<EmployeePosition, int>(_context);
                }

                return _employeePositionRepository;
            }
        }
        public IRepository<SubjectDistribution, int> SubjectDistributionRepository
        {
            get
            {
                if (_subjectDistributionRepository == null)
                {
                    _subjectDistributionRepository = new Repository<SubjectDistribution, int>(_context);
                }

                return _subjectDistributionRepository;
            }
        }

        public IRepository<Criteria, int> CriteriaRepository 
        {
            get
            {
                if (_criteriaRepository == null)
                {
                    _criteriaRepository = new Repository<Criteria, int>(_context);
                }

                return _criteriaRepository;
            }
        }

        public IRepository<Work, int> WorkRepository
        {
            get
            {
                if (_workRepository == null)
                {
                    _workRepository = new Repository<Work, int>(_context);
                }

                return _workRepository;
            }
        }
        public IRepository<WorkType, int> WorkTypeRepository 
        {
            get
            {
                if (_workTypeRepository == null)
                {
                    _workTypeRepository = new Repository<WorkType, int>(_context);
                }

                return _workTypeRepository;
            }
        }
        public IRepository<ExamType, int> ExamTypeRepository 
        {
            get
            {
                if (_examTypeRepository == null)
                {
                    _examTypeRepository = new Repository<ExamType, int>(_context);
                }

                return _examTypeRepository;
            }
        }

        public async Task<int> Save()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class, IEntity<TId>
        {
            string key = $"{typeof(TEntity).Name}{typeof(TId).Name}";
            object repositoryObj;
            Repository<TEntity, TId> repository;
            if (!_repositories.TryGetValue(key, out repositoryObj))
            {
                repository = new Repository<TEntity, TId>(_context);
                _repositories.TryAdd(key, repository);
                return repository;
            }
            else
            {
                repository = repositoryObj as Repository<TEntity, TId>;
            }

            return repository;
        }

        private readonly ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();

        private readonly AppDbContext _context;
       
        private IRepository<User, int> _userRepository;
        private IRepository<TokenCouple, int> _tokenCoupleRepository;
        private IRepository<Employee, int> _employeeRepository;
        private IRepository<Group, int> _groupRepository;
        private IRepository<Student, int> _studentRepository;
        private IRepository<Subject, int> _subjectRepository;
        private IRepository<EmployeePosition, int> _employeePositionRepository;
        private IRepository<SubjectDistribution, int> _subjectDistributionRepository;
        private IRepository<Criteria, int> _criteriaRepository;
        private IRepository<Work, int> _workRepository;
        private IRepository<WorkType, int> _workTypeRepository;
        private IRepository<ExamType, int> _examTypeRepository;

    }
}
