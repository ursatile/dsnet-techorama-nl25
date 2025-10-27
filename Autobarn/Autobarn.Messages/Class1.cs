namespace Autobarn.Messages;

public class NewVehicleMessage {
	public string? Registration { get; set; }
	public string? Manufacturer { get; set; }
	public string? ModelName { get; set; }
	public string? ModelCode { get; set; }
	public string? Color { get; set; }
	public int Year { get; set; }
	public DateTimeOffset ListedAt { get; set; }
	public override string ToString()
		=> $"{Registration} ({Manufacturer} {ModelName}, {Year}, {Color})";
}
