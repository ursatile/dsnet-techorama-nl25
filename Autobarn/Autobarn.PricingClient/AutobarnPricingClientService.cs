using Autobarn.Messages;
using EasyNetQ;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Autobarn.PricingClient;

public class AutobarnPricingClientService(IBus bus, ILogger<AutobarnPricingClientService> logger) : IHostedService {

	private const string SUBSCRIBER_ID = "autobarn.pricingclient";

	public async Task StartAsync(CancellationToken token) {
		await bus.PubSub.SubscribeAsync<NewVehicleMessage>(
			SUBSCRIBER_ID, HandleNewVehicleMessage, token);
		logger.LogInformation("Autobarn Pricing Client Service started and listening for new vehicle messages.");
	}

	public Task StopAsync(CancellationToken cancellationToken) {
		// No special stop logic required
		logger.LogInformation("Autobarn Pricing Client Service stopping.");
		return Task.CompletedTask;
	}

	private Task HandleNewVehicleMessage(NewVehicleMessage message) {
		Console.WriteLine($"New vehicle listed: {message}");
		return Task.CompletedTask;
	}
}