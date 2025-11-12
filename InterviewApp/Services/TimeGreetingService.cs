using System;

namespace InterviewApp.Services
{
    public class TimeGreetingService : ITimeGreetingService
    {
        public string GetTimeBasedGreeting()
        {
            var currentTime = DateTime.Now;
            if (currentTime.Hour < 12)
            {
                return "Good morning";
            }
            else if (currentTime.Hour < 18)
            {
                return "Good afternoon";
            }
            else
            {
                return "Good evening";
            }
        }
    }
}
