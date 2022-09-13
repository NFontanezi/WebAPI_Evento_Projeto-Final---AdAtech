using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface ICityEventService
    {
        List<CityEvent> GetAllEvents();
    }
}
