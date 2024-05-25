using Application.Services;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ServiceApplicationExtensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddScoped<IDataBaseConnService, Adapter.MongoDB.DataBaseConnService>();
        services.AddScoped<IDataBasePersonService, Adapter.MongoDB.PersonService>();
        services.AddScoped<IDataBaseImportEventService, Adapter.MongoDB.ImportEventService>();
        services.AddScoped<IImportService, ImportService>();
        services.AddTransient<IFileReadService, CSVReadService>();        
        return services;
    }
}
