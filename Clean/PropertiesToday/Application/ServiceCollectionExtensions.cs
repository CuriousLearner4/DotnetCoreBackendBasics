using System.Reflection;
using Application.PipelineBehaviour;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationPipelineBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>),typeof(CachePipelineBehaviour<,>));
        }
    }
}
