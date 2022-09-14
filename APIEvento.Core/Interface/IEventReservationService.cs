

using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface IEventReservationService
    {
        List<EventReservation> GetAllReservations();

        EventReservation GetReservationsByName(string name);

        EventReservation GetReservationsByTitleAndName(string Title, string PersonName);

        bool InsertReservation(EventReservation e);

        bool UpdateReservationQuantity(int idReservation, int Quantity);

        bool DeleteReservation(int id);

    }
}
