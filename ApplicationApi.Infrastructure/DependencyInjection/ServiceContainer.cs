using ApplicationApi.Application.Interfaces;
using ApplicationApi.Infrastructure.Data;
using ApplicationApi.Infrastructure.Repositories;
using Ecommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddSharedServices<AuthenticationDbContext>(config, config["MySerilog:FileName"]!);
            services.AddScoped<IUser, UserRepository>();
            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
        {
            app.UseSharedPolicies();
            return app;
        }
    }
}
