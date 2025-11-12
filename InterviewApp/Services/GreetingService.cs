using InterviewApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace InterviewApp.Services
{
    public class GreetingService
    {
        private readonly ILogger<GreetingService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITimeGreetingService _timeGreetingService;

        public GreetingService(ILogger<GreetingService> logger, IConfiguration configuration, ITimeGreetingService timeGreetingService)
        {
            _logger = logger;
            _configuration = configuration;
            _timeGreetingService = timeGreetingService;
        }

        public string GetGreetingMessage()
        {
            var language = _configuration["Greeting:Language"];
            var message = _configuration["Greeting:Message"];

            if (string.IsNullOrEmpty(language) || string.IsNullOrEmpty(message))
            {
                _logger.LogError("Message or Language configuration is missing.");
                Environment.Exit(1);
            }

            _logger.LogInformation("Greeting service started.");

            string timeGreeting = _timeGreetingService.GetTimeBasedGreeting();
            string greeting = language switch
            {
                "English" => "Hello",
                "Afrikaans" => "Hallo",
                "Zulu" => "Sawubona",
                _ => throw new InvalidOperationException($"Language '{language}' is not supported.")
            };

            _logger.LogInformation($"Greeting displayed: {timeGreeting} {greeting} {message}");
            return $"{timeGreeting} {greeting} {message}";
        }
    }

}