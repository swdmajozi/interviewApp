using InterviewApp.Services;
using MediatR;
public class GetTimeGreetingQuery : IRequest<string> { }
public class GetTimeGreetingHandler : IRequestHandler<GetTimeGreetingQuery, string>
{
    private readonly ITimeGreetingService _timeGreetingService;

    public GetTimeGreetingHandler(ITimeGreetingService timeGreetingService)
    {
        _timeGreetingService = timeGreetingService;
    }

    public Task<string> Handle(GetTimeGreetingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_timeGreetingService.GetTimeBasedGreeting());
    }
}