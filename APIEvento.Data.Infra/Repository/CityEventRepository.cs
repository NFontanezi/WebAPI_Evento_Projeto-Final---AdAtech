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

            public List<CityEvent> GetEventsByLocal(string Local) // add busca por data na service
            {
                var query = "SELECT * FROM cityEvent WHERE Title LIKE @Local";


                var parameters = new DynamicParameters();
                parameters.Add("@Local", $"%{Local}%");

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Query<CityEvent>(query, parameters).ToList();
            }

            
            public bool InsertEvent(CityEvent e)
            {
                var query = "INSERT INTO CityEvent VALUES (@Title, @DescriptionEvent, @DateHourEvent, @Local, @Adress, @Price, @StatusE)";

                var parameters = new DynamicParameters(e);

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;

            }

            public bool UpdateEvent(int idEvent, CityEvent e)
            {

                var query = @"UPDATE CityEvent set  Title = @Title, DescriptionEvent = @DescriptionEvent, DateHourEvent = @DateHourEvent, Local = @Local, Adress  =@Adress, Price = @Price, StatusE = @StatusE
            where idEvent = @idEvent";

                e.IdEvent = idEvent;

                var parameters = new DynamicParameters(e);

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }

            public bool DeleteEvent(int IdEvent) // implementar filtro de ativo/ inativo
            {
                var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent ";

                var parameters = new DynamicParameters();
                parameters.Add("IdEvent", IdEvent);

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
        }
    }

