﻿
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

        public EventReservation GetReservationsByName(string name)
        {
            return _eventRepository.GetReservationsByName(name);
        }

        public List<EventReservation> GetReservationsByTitle(string title)
        {
            return _eventRepository.GetReservationsByTitle(title);
        }
       

        public bool InsertReservation(EventReservation e)
        {
            return _eventRepository.InsertReservation(e);
        }

        public bool UpdateReservation(int id, EventReservation e)
        {
            return _eventRepository.UpdateReservation(id, e);
        }

        public bool DeleteReservation(int id)
        {
            return _eventRepository.DeleteReservation(id);
        }
    }
}