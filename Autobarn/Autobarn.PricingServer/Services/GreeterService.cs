using Autobarn.Pricing;
using Grpc.Core;

namespace Autobarn.PricingServer.Services;

public class PricerService(ILogger<PricerService> logger) : Pricer.PricerBase {

	public override Task<PriceReply> GetPrice(PriceRequest request, ServerCallContext context) {
		logger.LogInformation($"Calculating price for {request}");
		return Task.FromResult(new PriceReply {
			Currency = "EUR",
			Price = Random.Shared.Next(5000, 50000)
		});
	}
}

