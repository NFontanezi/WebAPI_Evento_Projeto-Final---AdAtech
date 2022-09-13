using APIEvent.Core.Interface;
using APIEvent.Core.Model;

namespace APIEvent.Core.Service
{
    public class CityEventService : ICityEventService

    {
        private readonly IReservationRepository _eventRepository;

        public CityEventService (IReservationRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }   

        public List<CityEvent> GetAllEvents()
        {
            return _eventRepository.GetAllEvents();
        }
    }
}
