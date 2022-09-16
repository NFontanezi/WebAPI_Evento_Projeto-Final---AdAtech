
using APIEvent.Core.Interface;
using APIEvent.Core.Model;

namespace APIEvent.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        private readonly IReservationRepository _eventRepository;

        private readonly ICityEventRepository _cityEventRepository;

        public EventReservationService(IReservationRepository eventRepository, ICityEventRepository cityEventRepository)
        {
            _eventRepository = eventRepository;
            _cityEventRepository = cityEventRepository;
        }

        public async Task<List<EventReservation>> GetAllReservationsAsync()
        {
            return await _eventRepository.GetAllReservationsAsync();
        }


        public async Task<List<Object>> GetReservationsByTitleAndNameAsync(string Title, string PersonName)
        {
            return await _eventRepository.GetReservationsByTitleAndNameAsync(Title, PersonName);
        }


        public async Task<bool> InsertReservationAsync(EventReservation e)

        {

            if (!CheckActiveEvent(e.IdEvent))
            {
                return false;
              
            }
                
            return await _eventRepository.InsertReservationAsync(e);
        }

       public async Task<bool> UpdateReservationQuantityAsync(int id, int quant)
        {
            return await _eventRepository.UpdateReservationQuantityAsync(id, quant);
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            return await _eventRepository.DeleteReservationAsync(id);
        }

        public bool CheckActiveEvent(long id)
        {

            var listaEvents = _cityEventRepository.GetAllEvents();

          return listaEvents.Where(x => x.IdEvent == id && x.Status == true).Any();
            
        }
    }
}
