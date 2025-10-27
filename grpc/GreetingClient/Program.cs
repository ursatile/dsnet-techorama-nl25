using GreetingDemo;
using Grpc.Net.Client;

var names = new string[] { "Alice", "Bryan", "Carol", "David", "Edgar", "Filip" };

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:5292");
var client = new Greeter.GreeterClient(channel);


while (true) {
	Console.WriteLine("Ready! Press a key to greet somebody - choose a language:");
	var languageCode = Console.ReadKey().KeyChar switch {
		'1' => "en-GB",
		'2' => "nl-NL",
		_ => "en-US"
	};
	var req = new HelloRequest {
		FirstName = names[Random.Shared.Next(names.Length)],
		LastName = "Techorama",
		LanguageCode = languageCode
	};
	var reply = await client.SayHelloAsync(req);
	Console.WriteLine(reply.Message);
}


