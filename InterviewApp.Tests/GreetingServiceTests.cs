using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using MediatR;
using InterviewApp.Services;

namespace InterviewApp.Tests
{
    public class GreetingServiceTests
    {
        [Fact]
        public void GetGreetingMessage_ShouldReturnCorrectGreeting_WhenValidConfiguration()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["Language"]).Returns("English");
            configurationMock.Setup(c => c["Message"]).Returns("World");

            var loggerMock = new Mock<ILogger<GreetingService>>();

            var timeGreetingServiceMock = new Mock<ITimeGreetingService>();
            timeGreetingServiceMock.Setup(t => t.GetTimeBasedGreeting()).Returns("Good morning");

            var greetingService = new GreetingService(loggerMock.Object, configurationMock.Object, timeGreetingServiceMock.Object);

            // Act
            var result = greetingService.GetGreetingMessage();

            // Assert
            Assert.Equal("Good morning Hello World", result);
        }

        [Fact]
        public void GetGreetingMessage_ShouldThrowException_WhenLanguageIsUnsupported()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["Language"]).Returns("Spanish");
            configurationMock.Setup(c => c["Message"]).Returns("World");

            var loggerMock = new Mock<ILogger<GreetingService>>();
            var timeGreetingServiceMock = new Mock<ITimeGreetingService>();

            var greetingService = new GreetingService(loggerMock.Object, configurationMock.Object, timeGreetingServiceMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => greetingService.GetGreetingMessage());
        }

        [Fact]
        public void GetGreetingMessage_ShouldLogError_WhenMessageOrLanguageIsMissing()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["Language"]).Returns(string.Empty);
            configurationMock.Setup(c => c["Message"]).Returns(string.Empty);

            var loggerMock = new Mock<ILogger<GreetingService>>();
            var timeGreetingServiceMock = new Mock<ITimeGreetingService>();

            var greetingService = new GreetingService(loggerMock.Object, configurationMock.Object, timeGreetingServiceMock.Object);

            // Act
            var result = greetingService.GetGreetingMessage();

            // Assert
            loggerMock.Verify(l => l.LogError(It.IsAny<string>()));
        }
    }
}
