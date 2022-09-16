using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface ICityEventService
    {
        List<CityEvent> GetAllEvents();

        Task<List<CityEvent>> GetEventsByTitleAsync(string Title);

        Task<List<CityEvent>> GetEventsByLocalAndDateAsync(string Local, DateTime DateHourEvent);

        Task<List<CityEvent>> GetEventsByPriceAndDataAsync(decimal Min, decimal Max, DateTime DateHourEvent);

        Task<bool> InsertEventAsync(CityEvent e);

        Task<bool> UpdateEventAsync(int idEvent, CityEvent e);

        Task<bool> DeleteEventAsync(int IdEvent);

    }
}
