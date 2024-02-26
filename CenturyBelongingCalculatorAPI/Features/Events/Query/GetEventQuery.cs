using AutoMapper;
using CenturyBelongingCalculatorAPI.ServiceManager;
using MediatR;

namespace CenturyBelongingCalculatorAPI.Features;

public class GetEventQuery: IQuery<EventResult>
{
    public int EventId { get; set; }
}
public class GetEventHandler : IRequestHandler<GetEventQuery, EventResult>
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public GetEventHandler(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    public async Task<EventResult> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var aevent = await _serviceManager.Event.GetEventByIdAsync(request.EventId);
        var results = _mapper.Map<EventResult>(aevent);
        return results;
    }
}
