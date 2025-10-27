using Autobarn.Pricing;
using EasyNetQ;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Logging;

var builder = Host.CreateApplicationBuilder(args);

//var amqp = builder.Configuration.GetConnectionString("AutobarnRabbitMQ");
//var bus = RabbitHutch.CreateBus(amqp);
//builder.Services.AddSingleton(bus);
//builder.Services.AddHostedService<AutobarnPricingClientService>();

//var host = builder.Build();
//host.Run();


var grpc = builder.Configuration["AutobarnPricingGrpcAddress"]!;
using var channel = GrpcChannel.ForAddress(grpc);
var client = new Pricer.PricerClient(channel);

while (true) {
	Console.WriteLine("Ready! Press a key to price a car:");
	Console.ReadKey(true);
	var req = new PriceRequest {
		Color = "Silver",
		Make = "DMC",
		Model = "DELOREAN",
		Year = 1985
	};
	var reply = await client.GetPriceAsync(req);
	Console.WriteLine(reply.Currency + reply.Price);
}

