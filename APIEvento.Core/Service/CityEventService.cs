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

        public  List<CityEvent> GetAllEvents()
        {
            return  _eventRepository.GetAllEvents();
        }

        public async Task<List<CityEvent>> GetEventsByTitleAsync(string Title)
        {
            return await _eventRepository.GetEventsByTitleAsync(Title);
        }

        public async Task<List<CityEvent>> GetEventsByLocalAndDateAsync(string Local, DateTime DateHourEvent)
        {
            return await _eventRepository.GetEventsByLocalAndDateAsync(Local, DateHourEvent);
        }

        public async Task<List<CityEvent>> GetEventsByPriceAndDataAsync(decimal Min, decimal Max, DateTime DateHourEvent)
        {
            return await _eventRepository.GetEventsByPriceAndDataAsync(Min, Max, DateHourEvent);
        }


        public async Task<bool> InsertEventAsync(CityEvent e)
        {
            return await _eventRepository.InsertEventAsync(e);
        }

        public async Task<bool> UpdateEventAsync(int idEvent, CityEvent e)
        {
            return await _eventRepository.UpdateEventAsync(idEvent, e);
        }

        public async Task<bool> DeleteEventAsync(int IdEvent)
        {
            return await _eventRepository.DeleteEventAsync(IdEvent);
        }



    }
}
