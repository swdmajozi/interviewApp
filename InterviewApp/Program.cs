using InterviewApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;

class Program
{
    public static async Task Main(string[] args)
    {
        // Create the host and configure services
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // Add appsettings.json to configuration
                config.AddJsonFile("C:\\Projects\\Book\\InterviewApp\\InterviewApp\\appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Register MediatR and all handlers
                services.AddMediatR(typeof(Program));

                // Register services
                services.AddSingleton<ITimeGreetingService, TimeGreetingService>();
                services.AddSingleton<GreetingService>();

                // Register ILogger
                services.AddLogging();

                // Add the configuration to the DI container
                services.AddSingleton<IConfiguration>(hostContext.Configuration);
            })
            .Build();

        // Create a scope to resolve services
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var mediator = services.GetRequiredService<IMediator>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                // Send a greeting request via MediatR
                var greeting = await mediator.Send(new GreetUserCommand());
                Console.WriteLine(greeting);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while handling the greeting request.");
            }
        }

        await host.RunAsync();
    }
}
