using Moq;
using Xunit;
using System;
using InterviewApp.Services;

namespace InterviewApp.Tests
{
    public class TimeGreetingServiceTests
    {
        [Theory]
        [InlineData(9, "Good morning")]
        [InlineData(14, "Good afternoon")]
        [InlineData(19, "Good evening")]
        public void GetTimeBasedGreeting_ShouldReturnCorrectGreeting(int hour, string expectedGreeting)
        {
            // Arrange
            var timeGreetingService = new TimeGreetingService();

            // Act
            var result = timeGreetingService.GetTimeBasedGreeting();

            // Assert
            Assert.Equal(expectedGreeting, result);
        }
    }
}
