using Autobarn.Messages;
using Autobarn.Pricing;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Autobarn.PricingClient;

public class AutobarnPricingClientService(
	IBus bus,
	ILogger<AutobarnPricingClientService> logger,
	Pricer.PricerClient pricerClient
) : IHostedService {
	private Pricer.PricerClient pricerClient = pricerClient;

	private const string SUBSCRIBER_ID = "autobarn.pricingclient_dylan";

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

	private async Task HandleNewVehicleMessage(NewVehicleMessage message) {
		logger.LogInformation($"Calculating a price for {message}...");
		var req = new PriceRequest {
			Color = message.Color,
			Make = message.Manufacturer + "DYLAN",
			Model = message.ModelName,
			Year = message.Year
		};
		var reply = await pricerClient.GetPriceAsync(req);
		logger.LogInformation("{currency} {price}", reply.Currency, reply.Price);
		var newVehiclePriceMessage = message.WithPrice(reply.Price, reply.Currency);
		await bus.PubSub.PublishAsync(newVehiclePriceMessage);
		logger.LogInformation("Published price to bus: {currency} {price}", reply.Currency, reply.Price);
	}
}