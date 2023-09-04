using OnlineForm.Services.Abstractions;
using OnlineForm.Services;
using OnlineFormApi.Services.Abstractions;
using OnlineFormApi.Services;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace OnlineFormApi.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterOnlineFormServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IContryService, CountryService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMapperService, MapperSerivce>();
            services.AddDbContext<FormDbContext>(opts => opts
            .UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"))
            .EnableSensitiveDataLogging(true));
            return services;
        }
    }
}
