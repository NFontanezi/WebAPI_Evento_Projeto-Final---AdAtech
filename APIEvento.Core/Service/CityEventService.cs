using APIEvent.Core.Interface;
using APIEvent.Core.Model;

namespace APIEvent.Core.Service
{
    public class CityEventService : ICityEventService

    {
        private readonly ICityEventRepository _eventRepository;

        public CityEventService (ICityEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }   

        public List<CityEvent> GetAllEvents()
        {
            return _eventRepository.GetAllEvents();
        }

        public List<CityEvent> GetEventsByTitle(string Title)
        {
            return _eventRepository.GetEventsByTitle(Title);
        }

        public List<CityEvent> GetEventsByLocalAndDate(string Local, DateTime DateHourEvent)
        {
            return _eventRepository.GetEventsByLocalAndDate(Local, DateHourEvent);
        }

        public List<CityEvent> GetEventsByPriceAndData(decimal Min, decimal Max, DateTime DateHourEvent)
        {
            return _eventRepository.GetEventsByPriceAndData(Min, Max, DateHourEvent);
        }


        public bool InsertEvent(CityEvent e)
        {
            return _eventRepository.InsertEvent(e);
        }

        public bool UpdateEvent(int idEvent, CityEvent e)
        {
            return _eventRepository.UpdateEvent(idEvent, e);
        }

        public bool DeleteEvent(int IdEvent)
        {
            return _eventRepository.DeleteEvent(IdEvent);
        }



    }
}
