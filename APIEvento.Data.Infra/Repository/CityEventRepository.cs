using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace APIEvent.Data.Infra.Repository
{

    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
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

        public List<CityEvent> GetEventsByTitle(string Title)
        {
            var query = "SELECT * FROM cityEvent WHERE Title LIKE @Title";


            var parameters = new DynamicParameters();
            parameters.Add("@Title", $"%{Title}%");

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventsByLocalAndDate(string Local, DateTime DateHourEvent)
        {
            var query = "SELECT * FROM cityEvent WHERE Local LIKE @Local AND DateHourEvent = @DateHourEvent";


            var parameters = new DynamicParameters();
            parameters.Add("@Local", $"%{Local}%");
            parameters.Add("@DateHourEvent", DateHourEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Query<CityEvent>(query, parameters).ToList();
        }
        public List<CityEvent> GetEventsByPriceAndData(decimal Min, decimal Max, DateTime DateHourEvent)
        {
            var query = "SELECT * FROM cityEvent WHERE Price BETWEEN @Min AND @Max AND DateHourEvent = @DateHourEvent";


            var parameters = new DynamicParameters();
            parameters.Add("@Min", Min);
            parameters.Add("@Max", Max);
            parameters.Add("@DateHourEvent", DateHourEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Query<CityEvent>(query, parameters).ToList();
        }


        public bool InsertEvent(CityEvent e)
        {
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Adress, @Price, @Status)";

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;

        }

        public bool UpdateEvent(int idEvent, CityEvent e)
        {

            var query = @"UPDATE CityEvent set  Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, Local = @Local, Adress  =@Adress, Price = @Price, Status = @Status
            where idEvent = @idEvent";

            e.IdEvent = idEvent;

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteEvent(int IdEvent) // implementar filtro de ativo/ inativo
        {
            if (CheckStatus(IdEvent)==true)
            {

                var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent ";

                var parameters = new DynamicParameters();
                parameters.Add("IdEvent", IdEvent);

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            else
            {

                var query2 = @"UPDATE CityEvent SET Status = 0 WHERE IdEvent = @IdEvent ";

                var parameters2 = new DynamicParameters();
                parameters2.Add("Status", 0);
                parameters2.Add("IdEvent", IdEvent);

                var connectionString2 = _configuration.GetConnectionString("DefaultConnection");

                using var conn2 = new SqlConnection(connectionString2);

                return conn2.Execute(query2, parameters2) == 1;
            }

        }

        public bool CheckStatus(int IdEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE IdEvent = @IdEvent ";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);


            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) >= 1;
        }
    }
}

