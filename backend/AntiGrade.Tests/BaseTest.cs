
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace UnitTests
{
    public abstract class BaseTest : IAsyncLifetime, IDisposable
    {
        private static readonly Lazy<IServiceProvider> ContainerInitializer =
            new Lazy<IServiceProvider>(CreateContainer, LazyThreadSafetyMode.ExecutionAndPublication);
        private readonly IServiceScope _serviceScope;
        protected IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;
        protected BaseTest()
        {
            _serviceScope = ContainerInitializer.Value.CreateScope();
        }
        public void Dispose()
        {
            _serviceScope?.Dispose();
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public static IServiceProvider CreateContainer()
        {
            var services = new ServiceCollection();

            var partManager = new ApplicationPartManager();
            partManager.FeatureProviders.Add(new ControllerFeatureProvider());
            var controllersFeature = new ControllerFeature();
            partManager.PopulateFeature(controllersFeature);
            foreach (var controllerType in controllersFeature.Controllers.Select(t => t.AsType()))
            {
                services.TryAddTransient(controllerType);
            }

            Startup.ConfigureServices(services);
            return services.BuildServiceProvider();
        }
    }
}