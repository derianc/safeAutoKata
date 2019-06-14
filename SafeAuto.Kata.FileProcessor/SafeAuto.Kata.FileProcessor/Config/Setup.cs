using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Repositories;
using SafeAuto.Kata.Repositories.Interfaces;
using SafeAuto.Kata.Services;
using SafeAuto.Kata.Services.Interfaces;

namespace SafeAuto.Kata.FileProcessor.Config
{
    public static class Setup
    {
        public static ServiceProvider ConfigureDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                // configure repositories
                .AddSingleton<IDriverRepository, DriverRepository>()

                // configure services
                .AddSingleton<IFileReaderService, FileReaderService>()
                .AddSingleton<IPrintService, PrintService>()
                .AddSingleton<IDriverService, DriverService>()

                // configure logging
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddLogging(loggingBuilder => loggingBuilder
                    .AddDebug()
                    .SetMinimumLevel(LogLevel.Debug))
                .BuildServiceProvider()
                ;

            return serviceProvider;
        }

        public static IFileReaderService GetFileReaderService(this ServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IFileReaderService>();
        }

        public static IPrintService GetPrintService(this ServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IPrintService>();
        }

        public static IDriverService GetDriverService(this ServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IDriverService>();
        }
    }
}
