using Autobarn.Pricing;
using Autobarn.PricingClient;
using EasyNetQ;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Logging;

var builder = Host.CreateApplicationBuilder(args);

var amqp = builder.Configuration.GetConnectionString("AutobarnRabbitMQ");
var bus = RabbitHutch.CreateBus(amqp);
builder.Services.AddSingleton(bus);

var grpc = builder.Configuration["AutobarnPricingGrpcAddress"]!;
using var channel = GrpcChannel.ForAddress(grpc);
var pricerClient = new Pricer.PricerClient(channel);
builder.Services.AddSingleton(pricerClient);
builder.Services.AddHostedService<AutobarnPricingClientService>();

var host = builder.Build();
host.Run();


