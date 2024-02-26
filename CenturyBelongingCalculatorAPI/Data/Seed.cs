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
            Name = "21th Century",
            Description = "Date of begin of 21th Century",
            EventDate = new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan(0))
        });
        #endregion
        context.SaveChanges();
    }
}
