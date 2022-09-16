

using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface IEventReservationService
    {
        Task<List<EventReservation>> GetAllReservationsAsync();

        Task<List<Object>> GetReservationsByTitleAndNameAsync(string Title, string PersonName);

        Task<bool> InsertReservationAsync(EventReservation e);

        Task<bool> UpdateReservationQuantityAsync(int idReservation, int Quantity);

        Task<bool> DeleteReservationAsync(int id);

        bool CheckActiveEvent(long id);

    }
}
