using AutoMapper;
using CenturyBelongingCalculatorAPI.ServiceManager;
using MediatR;

namespace CenturyBelongingCalculatorAPI.Features.Calcs.Query
{
    public class GetCalcListQuery: IQuery<IEnumerable<CalcResult>>
    {
    }
    public class GetCalcListHandler : IRequestHandler<GetCalcListQuery, IEnumerable<CalcResult>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public GetCalcListHandler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CalcResult>> Handle(GetCalcListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Event> calcs;

            calcs = await _serviceManager.Event.GetAllEventsAsync();

            var results = _mapper.Map<IEnumerable<CalcResult>>(calcs);

            return results;
        }
    }
}
