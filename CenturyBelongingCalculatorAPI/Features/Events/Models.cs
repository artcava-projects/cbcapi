namespace CenturyBelongingCalculatorAPI.Features;

public class EventResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset EventDate { get; set; }
}
