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

  

        public List<EventReservation> GetAllReservations()
        {
            var query = "SELECT * FROM EventReservation";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using var conn = new SqlConnection(connectionString);

                return conn.Query<EventReservation>(query).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return null;
            }
        }


        public List<Object> GetReservationsByTitleAndName(string Title, string PersonName)
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

            try
            {
                using var conn = new SqlConnection(connectionString);

                return conn.Query<Object>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return null;
            }
        }



        public bool InsertReservation(EventReservation e)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }

        }

        public bool UpdateReservationQuantity(int idReservation, int Quantity)
        {

            var query = @"UPDATE EventReservation set Quantity = @Quantity WHERE IdReservation = @IdReservation";


            var parameters = new DynamicParameters();

            parameters.Add("IdReservation", idReservation);
            parameters.Add("Quantity", Quantity);


            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }

        }


        public bool DeleteReservation(int IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation ";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", IdReservation);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }
        }


    }
}