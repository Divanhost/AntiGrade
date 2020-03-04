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

    }
}