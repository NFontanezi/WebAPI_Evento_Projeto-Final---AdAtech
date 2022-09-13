using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEvent.Data.Infra.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IConfiguration _configuration;

        public ReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        



        //METODOS EVENT RESERVATION 

        public List<EventReservation> GetAllReservations()
        {
            var query = "SELECT * FROM EventReservation";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Query<EventReservation>(query).ToList();
        }

        public EventReservation GetReservationsByName(string PersonName)
        {
            var query = "SELECT * FROM EventReservation WHERE PersonName = @PersonName";

            var parameters = new DynamicParameters();
            parameters.Add("PersonName", PersonName);


            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }

        public List<EventReservation> GetReservationsByTitle(string Title)
        { 
            var query = "SELECT * FROM EventReservation WHERE Title LIKE @Title";
           

            var parameters = new DynamicParameters();
            parameters.Add("@Title", $"%{Title}%"); //VERIFICAR JOIN PARA FUNCIONAR

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Query<EventReservation>(query, parameters).ToList();
        }



        public bool InsertReservation(EventReservation e)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;

        }

        public bool UpdateReservation(int idReservation, EventReservation e)
        {

            var query = @"UPDATE EventReservation set IdEvent = @IdEvent, PersonName = @PersonName, Quantity = @Quantity
            where idReservation = @idReservation";

            e.IdReservation = idReservation;

            var parameters = new DynamicParameters();
            
            parameters.Add("IdReservation", e.IdReservation);
            parameters.Add("IdEvent", e.IdEvent);
            parameters.Add("PersonName", e.PersonName);
            parameters.Add("Quantity", e.Quantity);
   

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;
        }


        public bool DeleteReservation(int IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation ";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", IdReservation);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;
        }


    }
}