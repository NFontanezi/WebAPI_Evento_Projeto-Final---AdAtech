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



        public  async Task<List<EventReservation>> GetAllReservationsAsync()
        {
            var query = "SELECT * FROM EventReservation";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return (await conn.QueryAsync<EventReservation>(query)).ToList();


        }


        public async Task<List<Object>> GetReservationsByTitleAndNameAsync(string Title, string PersonName)
        {

            var query = "SELECT* FROM EventReservation AS res " +
            "JOIN cityEvent AS city " +
            "ON res.idEvent = city.idEvent " +
            "WHERE city.Title LIKE ('%' +  @Title  + '%')" +
            "AND res.PersonName = @PersonName ";


            var parameters = new DynamicParameters();
            parameters.Add("@Title", Title);
            parameters.Add("@PersonName", PersonName);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");


            using var conn = new SqlConnection(connectionString);

            return (await conn.QueryAsync<Object>(query, parameters)).ToList();


        }



        public async Task<bool> InsertReservationAsync(EventReservation e)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return await conn.ExecuteAsync(query, parameters) == 1;



        }

        public async Task<bool> UpdateReservationQuantityAsync(int idReservation, int Quantity)
        {

            var query = @"UPDATE EventReservation set Quantity = @Quantity WHERE IdReservation = @IdReservation";


            var parameters = new DynamicParameters();

            parameters.Add("IdReservation", idReservation);
            parameters.Add("Quantity", Quantity);


            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return await conn.ExecuteAsync(query, parameters) == 1;

        }


        public async Task<bool> DeleteReservationAsync(int IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation ";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", IdReservation);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return await conn.ExecuteAsync(query, parameters) == 1;

        }


    }
}