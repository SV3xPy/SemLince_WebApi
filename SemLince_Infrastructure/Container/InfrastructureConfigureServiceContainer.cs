using Microsoft.Extensions.DependencyInjection;
using SemLince_Application.IRepositories;
using SemLince_Infrastructure.Repositories;

namespace SemLince_Infrastructure.Container
{
    public static class InfrastructureConfigureServiceContainer
    {
        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<SqlConnectionFactory>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
        }
    }
}
