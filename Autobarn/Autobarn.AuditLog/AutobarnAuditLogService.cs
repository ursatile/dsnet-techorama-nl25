using Autobarn.Messages;
using EasyNetQ;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Autobarn.AuditLog;

public class AutobarnAuditLogService(IBus bus, ILogger<AutobarnAuditLogService> logger) : IHostedService {

	private const string SUBSCRIBER_ID = "autobarn.notifier";

	public async Task StartAsync(CancellationToken token) {
		await bus.PubSub.SubscribeAsync<NewVehicleMessage>(
			SUBSCRIBER_ID, HandleNewVehicleMessage, token);
		logger.LogInformation("Autobarn Audit Log Service started and listening for new vehicle messages.");
	}

	public Task StopAsync(CancellationToken cancellationToken) {
		// No special stop logic required
		logger.LogInformation("Autobarn Audit Log Service stopping.");
		return Task.CompletedTask;
	}

	private Task HandleNewVehicleMessage(NewVehicleMessage message) {
		Console.WriteLine($"New vehicle listed: {message}");
		return Task.CompletedTask;
	}
}