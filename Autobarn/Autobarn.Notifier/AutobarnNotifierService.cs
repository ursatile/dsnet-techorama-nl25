using Autobarn.Messages;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Autobarn.Notifier;

public class AutobarnNotifierService(
	IBus bus,
	HubConnection hub,
	ILogger<AutobarnNotifierService> logger
) : IHostedService {

	private const string SUBSCRIBER_ID = "autobarn.notifier";

	public async Task StartAsync(CancellationToken token) {
		await bus.PubSub.SubscribeAsync<NewVehiclePriceMessage>(
			SUBSCRIBER_ID, HandleNewVehiclePriceMessage, token);

		await hub.StartAsync(token);
		logger.LogInformation("Autobarn Notifier Service started and listening for new vehicle messages.");
		
	}

	public async Task StopAsync(CancellationToken cancellationToken) {
		logger.LogInformation("Autobarn Notifier Service stopping.");
		await hub.StopAsync(cancellationToken);
	}

	private async Task HandleNewVehiclePriceMessage(NewVehicleMessage message) {
		Console.WriteLine($"Got price of vehicle: {message}");
		var json = JsonConvert.SerializeObject(message);
		await hub.SendAsync("NotifyWebsiteUsers", "autobarn.notifier", json);
	}
}