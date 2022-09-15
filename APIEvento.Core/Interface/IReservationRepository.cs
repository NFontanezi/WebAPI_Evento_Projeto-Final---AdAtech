

using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
   public interface IReservationRepository
    {

        //METODOS EVENT RESERVATION
        List<EventReservation> GetAllReservations();

        List<Object> GetReservationsByTitleAndName(string Title, string PersonName);

        bool InsertReservation(EventReservation e);

        bool UpdateReservationQuantity(int idReservation, int Quantity);

        bool DeleteReservation(int id);
    }
}
