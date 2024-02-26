using CenturyBelongingCalculatorAPI.Domain;

namespace CenturyBelongingCalculatorAPI.Data;

public class Seed
{
    public void SeedData(DataContext context)
    {
        #region Seeding Events
        context.Events.Add(new Event
        {
            Id = 1,
            Name = "21st Century",
            Description = "Start date of 21st Century",
            EventDate = new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan(0))
        });
        #endregion
        context.SaveChanges();
    }
}
