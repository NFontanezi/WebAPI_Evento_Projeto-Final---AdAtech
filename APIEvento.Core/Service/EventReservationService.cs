
using APIEvent.Core.Interface;
using APIEvent.Core.Model;

namespace APIEvent.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        private readonly IReservationRepository _eventRepository;

        public EventReservationService(IReservationRepository eventRepository)
        {
            _eventRepository = eventRepository;
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
    }
}
