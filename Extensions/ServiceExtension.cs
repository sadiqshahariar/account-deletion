using AccountDeletion.Repositories.Interface;
using AccountDeletion.Repositories.Repository;

namespace AccountDeletion.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection APIModuleServices(this IServiceCollection services)
        {
            try
            {
                services.AddScoped<IAccountRepository, AccountRepository>();  
                return services;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
