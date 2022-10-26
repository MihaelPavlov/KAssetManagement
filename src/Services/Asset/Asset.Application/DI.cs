namespace Asset.Application
{
    using Asset.Application.Behaviours;
    using Asset.Application.Mappings;
    using Asset.Application.Queries;
    using Asset.EventBus.Messages;
    using AutoMapper;
    using FluentValidation;
    using MassTransit;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DI
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper((cfg) =>
                cfg.AddProfiles(new List<Profile>{ new AutoMappingProfile(typeof(GetAssetListQuery)) , new MappingProfile()}));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Validations
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            // MassTransit-RabbitMQ Configuration

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });
        }
    }
}
