using AutoMapper;
using CenturyBelongingCalculatorAPI.ServiceManager;
using MediatR;

namespace CenturyBelongingCalculatorAPI.Features;

public class AddCalcCommand : ICommand<CalcResult>
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; } = DateTimeOffset.Now;
    public int EventId { get; set; }
    public required string CalcName {  get; set; }
}

public class AddCalcHandler : IRequestHandler<AddCalcCommand, CalcResult>
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public AddCalcHandler(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    public async Task<CalcResult> Handle(AddCalcCommand request, CancellationToken cancellationToken)
    {
        #region Checks
        if (request.StartDate >= request.EndDate)
            throw new EndDateGreaterThenStartDateException();

        var aevent = await _serviceManager.Event.GetEventByIdAsync(request.EventId);
        if (aevent == null)
            throw new NoEventExistsException(request.EventId);

        if ((request.StartDate >= aevent.EventDate) || (aevent.EventDate >= request.EndDate))
            throw new NotAllowedCalcException(aevent.Name);
        #endregion

        var result = new CalcResult
        {
            CalcName = request.CalcName,
            EventDate = aevent.EventDate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            EventName = aevent.Name,
            EventDescription = aevent.Description
        };

        return result;
    }
}