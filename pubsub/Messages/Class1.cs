namespace Messages;

public class Greeting {

	public int Number { get; set; } = 0;
	public DateTimeOffset GreetingTime { get; set; } = DateTimeOffset.Now;
	public string Name { get; set; } = String.Empty;

	public override string ToString()
		=> $"Greeting #{Number} from {Name} at {GreetingTime:O}";
}
