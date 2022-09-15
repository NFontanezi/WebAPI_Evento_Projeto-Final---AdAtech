
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
        //METODOS DE ACESSO
        public List<EventReservation> GetAllReservations()
        {
            return _eventRepository.GetAllReservations();
        }


        public List<Object> GetReservationsByTitleAndName(string Title, string PersonName)
        {
            return _eventRepository.GetReservationsByTitleAndName(Title, PersonName);
        }
       

        public bool InsertReservation(EventReservation e)
   
        {
            

            if (!CheckActiveEvent(e.IdEvent))
            {
                return false;
              
            }
                
            return _eventRepository.InsertReservation(e);
        }

        public bool UpdateReservationQuantity(int id, int quant)
        {
            return _eventRepository.UpdateReservationQuantity(id, quant);
        }

        public bool DeleteReservation(int id)
        {
            return _eventRepository.DeleteReservation(id);
        }

        public bool CheckActiveEvent(long id)
        {

            var listaEvents = _cityEventRepository.GetAllEvents();

          return listaEvents.Where(x => x.IdEvent == id && x.Status == true).Any();
            
        }
    }
}
