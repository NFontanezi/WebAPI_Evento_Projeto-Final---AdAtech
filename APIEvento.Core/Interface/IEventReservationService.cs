

using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface IEventReservationService
    {
        List<EventReservation> GetAllReservations();

        EventReservation GetReservationsByName(string name);

        List<EventReservation> GetReservationsByTitle(string title);

        bool InsertReservation(EventReservation e);

        bool UpdateReservationQuantity(int idReservation, int Quantity);

        bool DeleteReservation(int id);

    }
}
