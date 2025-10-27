using EasyNetQ;
using Messages;
const string AMQP = "amqps://rkrjbzva:OvLD545larVV0wAIWVFWKzT6HbHAVTgv@young-gray-dinosaur.rmq2.cloudamqp.com/rkrjbzva";
var bus = RabbitHutch.CreateBus(AMQP);

Console.WriteLine("Welcome to the subscriber!");

const string SUBSCRIBER_ID = "dylan";
await bus.PubSub.SubscribeAsync<Greeting>(SUBSCRIBER_ID, message => {
	if (message.Number % 5 == 0) {
		Console.WriteLine("MESSAGE FAILED ===================");
		throw new Exception("Bad message exception");
	}
	Console.WriteLine(message);
});

Console.WriteLine("Listening for messages. Press a key to exit.");
Console.ReadKey();
