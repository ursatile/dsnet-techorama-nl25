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

	public NewVehiclePriceMessage WithPrice(int price, string currency) => new() {
		Price = price,
		Currency = currency,
		Manufacturer = this.Manufacturer,
		ModelName = this.ModelName,
		ModelCode = this.ModelCode,
		Color = this.Color,
		Year = this.Year,
		ListedAt = this.ListedAt,
		Registration = this.Registration
	};
}

public class NewVehiclePriceMessage : NewVehicleMessage {
	public int Price { get; set; }
	public string Currency { get; set; } = default!;
}

