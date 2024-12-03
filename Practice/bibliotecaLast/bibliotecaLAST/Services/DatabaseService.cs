using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using static WebApplication1.Services.DatabaseService;

namespace WebApplication1.Services
{

    public class DatabaseService : IDatabaseService
    {
        private readonly SqlConnection _sqlConnection;

        public DatabaseService(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            try
            {
                // Consulta simple para obtener datos
                var query = "SELECT * FROM bibliotecaTable";
                var result = await _sqlConnection.QueryAsync<string>(query);
                return result;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new ApplicationException($"Error al consultar la base de datos: {ex.Message}");
            }
        }
    }
}
