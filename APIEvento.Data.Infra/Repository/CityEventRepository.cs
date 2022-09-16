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


        public List<CityEvent> GetAllEvents()
        {
            var query = "SELECT * FROM cityEvent";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return (conn.Query<CityEvent>(query)).ToList();

        }

        public async Task<List<CityEvent>> GetEventsByTitleAsync(string Title)
        {
            var query = "SELECT * FROM cityEvent WHERE Title LIKE @Title";


            var parameters = new DynamicParameters();
            parameters.Add("@Title", $"%{Title}%");

            var connectionString = _configuration.GetConnectionString("DefaultConnection");


            using var conn = new SqlConnection(connectionString);

            return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();

        }

        public async Task<List<CityEvent>> GetEventsByLocalAndDateAsync(string Local, DateTime DateHourEvent)
        {
            var query = "SELECT * FROM cityEvent WHERE Local LIKE ('%' + @Local + '%') AND CAST(DateHourEvent as DATE) = CAST(@DateHourEvent AS DATE)";


            var parameters = new DynamicParameters();
            parameters.Add("@Local", Local);
            parameters.Add("@DateHourEvent", DateHourEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");


            using var conn = new SqlConnection(connectionString);

            return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();

        }
        public async Task<List<CityEvent>> GetEventsByPriceAndDataAsync(decimal Min, decimal Max, DateTime DateHourEvent)
        {
            var query = "SELECT * FROM cityEvent WHERE Price BETWEEN @Min AND @Max AND CAST(DateHourEvent as DATE) = CAST(@DateHourEvent AS DATE)";


            var parameters = new DynamicParameters();
            parameters.Add("@Min", Min);
            parameters.Add("@Max", Max);
            parameters.Add("@DateHourEvent", DateHourEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");


            using var conn = new SqlConnection(connectionString);

            return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();

        }


        public async Task<bool> InsertEventAsync(CityEvent e)
        {
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Adress, @Price, @Status)";

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            return await conn.ExecuteAsync(query, parameters) == 1;
  

        }

        public async Task<bool> UpdateEventAsync(int idEvent, CityEvent e)
        {

            var query = @"UPDATE CityEvent set  Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, Local = @Local, Adress  =@Adress, Price = @Price, Status = @Status
            where idEvent = @idEvent";

            e.IdEvent = idEvent;

            var parameters = new DynamicParameters(e);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return await conn.ExecuteAsync(query, parameters) == 1;
 
        }

        public async Task <bool> DeleteEventAsync(int IdEvent) // VERIFICAR ERRO
        {
            if (CheckStatus(IdEvent) == false)
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

                    return await conn2.ExecuteAsync(query2, parameters2) == 1;
               
            }

        }

        public bool CheckStatus(int IdEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);


            var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) >= 1;

        }
    }
}

