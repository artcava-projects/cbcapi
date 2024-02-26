using CenturyBelongingCalculatorAPI.Data;
using CenturyBelongingCalculatorAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CenturyBelongingCalculatorAPI.Features;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<Event> GetEventByIdAsync(int eventId);
}

public class EventService : IEventService
{
    #region Ctor
    private readonly DataContext _context;

    public EventService(DataContext context)
    {
        _context = context;
    }
    #endregion

    #region Query
    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _context.Events
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        return await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
    }
    #endregion
}

