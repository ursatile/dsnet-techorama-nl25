using Autobarn.Notifier;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var amqp = builder.Configuration.GetConnectionString("AutobarnRabbitMQ");

var bus = RabbitHutch.CreateBus(amqp);
builder.Services.AddSingleton(bus);
builder.Services.AddHostedService<AutobarnNotifierService>();

var hubUrl = builder.Configuration["AutobarnSignalRHubUrl"]!;

var hub = new HubConnectionBuilder().WithUrl(hubUrl).Build();
builder.Services.AddSingleton(hub);

var host = builder.Build();
host.Run();