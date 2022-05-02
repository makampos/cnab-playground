using Domain.Intefaces.Repository;
using Infra.Repository;

namespace Api.Extensions
{
    public static class RepositoryConfigurationExtension
    {
        public static void AddRepositoryDependecyGroup(this IServiceCollection services) 
        {
            services.AddScoped<ITransactionRepository, TransacoRepository>();
        }
    }
}
