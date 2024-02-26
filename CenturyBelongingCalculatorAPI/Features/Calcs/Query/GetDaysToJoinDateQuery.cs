using AutoMapper;
using CenturyBelongingCalculatorAPI.ServiceManager;
using MediatR;

namespace CenturyBelongingCalculatorAPI.Features;

public class GetDaysToJoinDateQuery:IQuery<int>
{
    public DateTimeOffset StartDate { get; set; }
    public int EventId { get; set; }
}

public class GetDaysToJoinDateHandler : IRequestHandler<GetDaysToJoinDateQuery, int>
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public GetDaysToJoinDateHandler(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    public async Task<int> Handle(GetDaysToJoinDateQuery request, CancellationToken cancellationToken)
    {
        var aevent = await _serviceManager.Event.GetEventByIdAsync(request.EventId);
        if (aevent == null)
            throw new NoEventExistsException(request.EventId);

        if (request.StartDate >= aevent.EventDate)
            throw new NotAllowedCalcException(aevent.Name);

        var _now = DateTimeOffset.UtcNow;
        var joinDate = aevent.EventDate.AddDays((aevent.EventDate - request.StartDate).Days);
        var result = (joinDate - _now).Days;
        if (result <= 0)
            throw new JoinDateElapsedException(aevent.Name);

        return result;
    }
}

