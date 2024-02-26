using AutoMapper;
using CenturyBelongingCalculatorAPI.Domain;

namespace CenturyBelongingCalculatorAPI.Features
{
    public class MapperProfile: Profile
    {
        public MapperProfile() 
        {
            CreateMap<Event, EventResult>();
        }
    }
}
