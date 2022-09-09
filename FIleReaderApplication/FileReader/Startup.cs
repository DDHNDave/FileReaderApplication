using FileReader.Interfaces;
using FileReader.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace FileReader
{
    public class Startup
    {
        public Startup(HostBuilderContext context, IServiceCollection services)
        {
            this.HostBuilderContext = context;
            this.ServiceCollection = services;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IFileReader), this.TypedServiceFactory<FileReaderService>);
            services.AddScoped(typeof(IFileScanner), this.TypedServiceFactory<FileScanner>);
            services.AddScoped(typeof(FileMatchingResultMessagePrinter), this.TypedServiceFactory<FileMatchingResultMessagePrinter>);
            services.AddScoped<ScanFiles>();
        }

        protected readonly HostBuilderContext HostBuilderContext;

        protected T TypedServiceFactory<T>(IServiceProvider serviceProvider)
            where T : class
        {
            object[] parameters = GetConstructorArgumentForType(typeof(T), serviceProvider);
            return parameters.Any() ? 
                Activator.CreateInstance(typeof(T), parameters) as T : 
                Activator.CreateInstance(typeof(T)) as T;
        }

        protected object[] GetConstructorArgumentForType(Type type, IServiceProvider serviceProvider)
        {
            return type
                .GetConstructors()
                .FirstOrDefault(constructor => constructor.GetParameters().Any())
                ?.GetParameters()
                .Where(constructorParameter => !constructorParameter.IsOptional)
                .Select(parameter => serviceProvider.GetService(parameter.ParameterType))
                .ToArray() ?? new object[] { };
        }

        protected readonly IServiceCollection ServiceCollection;
    }
}
