namespace Asset.Application
{
    using Asset.Application.Behaviours;
    using Asset.Application.Mappings;
    using Asset.Application.Queries;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper((cfg) =>
                cfg.AddProfile(new AutoMappingProfile(typeof(GetAssetListQuery))));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Validations
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
