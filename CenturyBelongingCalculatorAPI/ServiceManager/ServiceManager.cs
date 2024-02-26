using CenturyBelongingCalculatorAPI.Data;
using CenturyBelongingCalculatorAPI.Features;

namespace CenturyBelongingCalculatorAPI.ServiceManager;

public class ServiceManager: IServiceManager
{
    private readonly DataContext _context;
    private IEventService _eventService;

    public ServiceManager(DataContext context)
    {
        _context = context;
    }

    public IEventService Event
    {
        get
        {
            if (_eventService == null)
                _eventService = new EventService(_context);

            return _eventService;
        }
    }
}
