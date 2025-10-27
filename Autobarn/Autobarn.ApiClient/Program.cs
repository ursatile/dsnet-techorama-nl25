using Autobarn.ApiClient;
using Microsoft.Extensions.Logging;

var http = new HttpClient() {
	BaseAddress = new Uri("https://autobarn.dev")
};
var logger = LoggerFactory.Create(lb
	=> lb.AddConsole()).CreateLogger<AutobarnApiClient>();
var client = new AutobarnApiClient(http, logger);
Console.WriteLine("Press a key to create a random car:");
while (true) {
	var location = await client.CreateRandomVehicleAsync();
	Console.WriteLine($"Created car at {location}");
	// Thread.Sleep(TimeSpan.FromSeconds(1));
	Console.ReadKey();
}

