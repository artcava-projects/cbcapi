using AutoMapper;
using CenturyBelongingCalculatorAPI.ServiceManager;
using MediatR;

namespace CenturyBelongingCalculatorAPI.Features;

public class GetEventsQuery: IQuery<IEnumerable<EventResult>>
{
}
public class GetEventsHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventResult>>
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public GetEventsHandler(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventResult>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Event> events;

        events = await _serviceManager.Event.GetAllEventsAsync();

        var results = _mapper.Map<IEnumerable<EventResult>>(events);
        
        return results;
    }
}
