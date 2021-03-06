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
        public IRepository<StudentCriteria, int> StudentCriteriaRepository 
        {
            get
            {
                if (_studentCriteriaRepository == null)
                {
                    _studentCriteriaRepository = new Repository<StudentCriteria, int>(_context);
                }

                return _studentCriteriaRepository;
            }
        }

        public IRepository<SubjectEmployee, int> SubjectEmployeeRepository 
        {
            get
            {
                if (_subjectEmployeeRepository == null)
                {
                    _subjectEmployeeRepository = new Repository<SubjectEmployee, int>(_context);
                }

                return _subjectEmployeeRepository;
            }
        }
        public IRepository<ExamResult, int> ExamResultRepository 
        {
            get
            {
                if (_examResultRepository == null)
                {
                    _examResultRepository = new Repository<ExamResult, int>(_context);
                }

                return _examResultRepository;
            }
        }

        public IRepository<Mode, int> ModeRepository 
        {
            get
            {
                if (_modeRepository == null)
                {
                    _modeRepository = new Repository<Mode, int>(_context);
                }

                return _modeRepository;
            }
        }
         public IRepository<Status, int> StatusRepository 
        {
            get
            {
                if (_statusRepository == null)
                {
                    _statusRepository = new Repository<Status, int>(_context);
                }

                return _statusRepository;
            }
        }
         public IRepository<Institute, int> InstituteRepository 
        {
            get
            {
                if (_instituteRepository == null)
                {
                    _instituteRepository = new Repository<Institute, int>(_context);
                }

                return _instituteRepository;
            }
        }
         public IRepository<Department, int> DepartmentRepository 
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new Repository<Department, int>(_context);
                }

                return _departmentRepository;
            }
        }
        public IRepository<Course, int> CourseRepository 
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new Repository<Course, int>(_context);
                }

                return _courseRepository;
            }
        }
        public IRepository<Semester, int> SemesterRepository 
        {
            get
            {
                if (_semesterRepository == null)
                {
                    _semesterRepository = new Repository<Semester, int>(_context);
                }

                return _semesterRepository;
            }
        }
        public IRepository<SubjectExamStatus, int> SubjectExamStatusRepository 
        {
            get
            {
                if (_subjectExamStatusRepository == null)
                {
                    _subjectExamStatusRepository = new Repository<SubjectExamStatus, int>(_context);
                }

                return _subjectExamStatusRepository;
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
        private IRepository<Criteria, int> _criteriaRepository;
        private IRepository<Work, int> _workRepository;
        private IRepository<WorkType, int> _workTypeRepository;
        private IRepository<ExamType, int> _examTypeRepository;
        private IRepository<StudentCriteria, int> _studentCriteriaRepository;
        private IRepository<SubjectEmployee, int> _subjectEmployeeRepository;
        private IRepository<ExamResult, int> _examResultRepository;
        private IRepository<Mode, int> _modeRepository;
        private IRepository<Status, int> _statusRepository;
        private IRepository<Institute, int> _instituteRepository;
        private IRepository<Department, int> _departmentRepository;
        private IRepository<Course, int> _courseRepository;
        private IRepository<Semester, int> _semesterRepository;
        private IRepository<SubjectExamStatus, int> _subjectExamStatusRepository;

    }
}
