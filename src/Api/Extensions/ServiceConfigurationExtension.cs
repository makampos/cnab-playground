using Domain.Intefaces.Services;
using Domain.Services;

namespace Api.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static void AddServicesDependecyGroup(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
