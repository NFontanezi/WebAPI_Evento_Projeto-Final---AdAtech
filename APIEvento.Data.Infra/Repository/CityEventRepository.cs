using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace APIEvent.Data.Infra.Repository
{
    public class CityEventRepository
    {

        public class ReservationRepository : IReservationRepository
        {
            private readonly IConfiguration _configuration;

            public ReservationRepository(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            //METODOS CITY EVENT
            public List<CityEvent> GetAllEvents()
            {
                var query = "SELECT * FROM cityEvent";

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Query<CityEvent>(query).ToList();
            }
        }
}
