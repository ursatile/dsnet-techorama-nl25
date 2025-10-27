
using EasyNetQ;
using Messages;
const string AMQP = "amqps://rkrjbzva:OvLD545larVV0wAIWVFWKzT6HbHAVTgv@young-gray-dinosaur.rmq2.cloudamqp.com/rkrjbzva";
var bus = RabbitHutch.CreateBus(AMQP);

var number = 0;
Console.WriteLine("Press a key to publish a message...");
while (true) {
	Console.ReadKey();
	var greeting = new Greeting() {
		Name = Environment.MachineName,
		Number = number++
	};
	await bus.PubSub.PublishAsync(greeting);
	Console.WriteLine($"Published {greeting}");
}







