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

        context.Events.Add(new Event
        {
            Id = 2,
            Name = "1st Halving",
            Description = "Btc halving 2012",
            EventDate = new DateTimeOffset(2012, 11, 28, 0, 0, 0, new TimeSpan(0))
        });

        context.Events.Add(new Event
        {
            Id = 3,
            Name = "2nd Halving",
            Description = "Btc halving 2016",
            EventDate = new DateTimeOffset(2016, 7, 9, 0, 0, 0, new TimeSpan(0))
        });

        context.Events.Add(new Event
        {
            Id = 4,
            Name = "3rd Halving",
            Description = "Btc halving 2020",
            EventDate = new DateTimeOffset(2020, 5, 11, 0, 0, 0, new TimeSpan(0))
        });
        #endregion
        context.SaveChanges();
    }
}
