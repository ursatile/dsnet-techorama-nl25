using GreetingDemo;
using Grpc.Core;

namespace GreetingServer.Services;

public class GreeterService : Greeter.GreeterBase {
	private readonly ILogger<GreeterService> _logger;
	public GreeterService(ILogger<GreeterService> logger) {
		_logger = logger;
	}

	public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) {
		var greeting = request.LanguageCode switch {
			"en-GB" => $"Good morning, {request.Name}",
			"nl-NL" => $"Goede morgen, {request.Name}",
			_ => $"Hello {request.Name} (p.s. please update your protocol file)"
		};

		return Task.FromResult(new HelloReply {
			Message = greeting
		});
	}
}
