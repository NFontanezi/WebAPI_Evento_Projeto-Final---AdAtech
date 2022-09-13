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

        public List<CityEvent> GetEventsByLocal(string Local)
        {
            return _eventRepository.GetEventsByLocal(Local);
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
