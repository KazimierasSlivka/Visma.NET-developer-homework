using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data;
using ContactManager.Services;

namespace ContactManager.ConsoleApplication
{
    class Program
    { 
        public static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<ContactsManager>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<IContactCrud, ContactCrud>();

            services.AddTransient<ContactsManager>();

            return services;
        }
    }
}
