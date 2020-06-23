

using System.Threading.Tasks;
using AntiGrade.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests
{
    public class DatabaseBaseTest : BaseTest
    {
        // private readonly ConcurrentDictionary<Guid, TestDbContext> _dbContexts = new ConcurrentDictionary<Guid, TestDbContext>();
        // private readonly ITestDbContextFactory _testDbContextFactory;
        protected AppDbContext TDbContext { get; }

        protected DatabaseBaseTest()
        {
            TDbContext = ServiceProvider.GetRequiredService<AppDbContext>();
            //_testDbContextFactory = ServiceProvider.GetRequiredService<ITestDbContextFactory>();
        }



        public override async Task DisposeAsync()
        {
            if (TDbContext.Database.IsSqlite())
            {
                TDbContext.Database.CloseConnection();
                await TDbContext.Database.EnsureDeletedAsync();
            }
        }

        public override async Task InitializeAsync()
        {
            if (TDbContext.Database.IsSqlite())
            {
                TDbContext.Database.OpenConnection();
                await TDbContext.Database.EnsureCreatedAsync();
            }

            await SeedAsync();
        }

        protected virtual Task SeedAsync() => Task.CompletedTask;
    }
}