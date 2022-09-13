

using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
     public interface ICityEventRepository
    {
        List<CityEvent> GetAllEvents();

        List<CityEvent> GetEventsByTitle(string Title);

        List<CityEvent> GetEventsByLocal(string Local);

        bool InsertEvent(CityEvent e);

        bool UpdateEvent(int idEvent, CityEvent e);

        bool DeleteEvent(int IdEvent);




    }
}
