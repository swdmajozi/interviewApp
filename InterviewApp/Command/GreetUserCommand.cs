using InterviewApp.Services;
using MediatR;
public class GreetUserCommand : IRequest<string> { }
public class GreetUserHandler : IRequestHandler<GreetUserCommand, string>
{
    private readonly IMediator _mediator;
    private readonly GreetingService _greetingService;

    public GreetUserHandler(GreetingService greetingService, IMediator mediator)
    {
        _greetingService = greetingService;
        _mediator = mediator;
    }

    public async Task<string> Handle(GreetUserCommand request, CancellationToken cancellationToken)
    {
        var timeGreeting = await _mediator.Send(new GetTimeGreetingQuery());
        var greetingMessage = _greetingService.GetGreetingMessage();
        return $"{timeGreeting} {greetingMessage}";
    }
}
