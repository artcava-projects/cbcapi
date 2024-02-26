using CenturyBelongingCalculatorAPI.Features;

namespace CenturyBelongingCalculatorAPI.ServiceManager;

public interface IServiceManager
{
    IEventService Event { get; }
}
