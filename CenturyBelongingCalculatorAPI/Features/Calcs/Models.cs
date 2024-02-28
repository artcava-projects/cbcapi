namespace CenturyBelongingCalculatorAPI.Features;

public class CalcResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string CalcName { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EventDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public int DaysBeforeEvent { get { return (EventDate - StartDate).Days; } }
    public int DaysAfterEvent { get { return (EndDate - EventDate).Days; } }
}
