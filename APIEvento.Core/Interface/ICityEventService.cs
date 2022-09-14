using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface ICityEventService
    {
        List<CityEvent> GetAllEvents();

        List<CityEvent> GetEventsByTitle(string Title);

        List<CityEvent> GetEventsByLocalAndDate(string Local, DateTime DateHourEvent);

        List<CityEvent> GetEventsByPriceAndData(decimal Min, decimal Max, DateTime DateHourEvent);

        bool InsertEvent(CityEvent e);

        bool UpdateEvent(int idEvent, CityEvent e);

        bool DeleteEvent(int IdEvent);
    }
}
