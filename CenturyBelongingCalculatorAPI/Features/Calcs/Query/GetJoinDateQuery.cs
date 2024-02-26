using AutoMapper;
using CenturyBelongingCalculatorAPI.ServiceManager;
using MediatR;

namespace CenturyBelongingCalculatorAPI.Features;

public class GetJoinDateQuery : IQuery<DateTimeOffset>
{
    public DateTimeOffset StartDate { get; set; }
    public int EventId { get; set; }
}

public class GetJoinDateHandler : IRequestHandler<GetJoinDateQuery, DateTimeOffset>
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public GetJoinDateHandler(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    public async Task<DateTimeOffset> Handle(GetJoinDateQuery request, CancellationToken cancellationToken)
    {
        var aevent = await _serviceManager.Event.GetEventByIdAsync(request.EventId);
        if (aevent == null)
            throw new NoEventExistsException(request.EventId);

        if (request.StartDate >= aevent.EventDate)
            throw new NotAllowedCalcException(aevent.Name);

        var result = aevent.EventDate.AddDays((aevent.EventDate - request.StartDate).Days);

        return result;
    }
}

