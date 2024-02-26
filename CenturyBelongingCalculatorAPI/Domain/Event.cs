namespace CenturyBelongingCalculatorAPI.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset EventDate { get; set; }

    }
}
