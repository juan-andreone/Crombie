using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using static WebApplication1.Services.LibroDBService;


namespace WebApplication1.Services
{

    public class LibroDBService : ILibroDBService
    {
        private readonly SqlConnection _sqlConnection;

        public LibroDBService(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<string>> GetNombresAsync()
        {
            try
            {
                var query = "SELECT Titulo FROM BookTable";
                var result = await _sqlConnection.QueryAsync<string>(query);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al consultar la base de datos: {ex.Message}");
            }
        }

        public async Task<string> GetNombreByIdAsync(int id)
        {
            try
            {
                var query = "SELECT Nombre_Autor FROM BookTable WHERE ID = @Id";
                return await _sqlConnection.QueryFirstOrDefaultAsync<string>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al consultar la base de datos: {ex.Message}");
            }
        }

        public async Task DeleteLibroByIdAsync(int id)
        {
            try
            {
                var query = "DELETE FROM BookTable WHERE ID = @Id";
                await _sqlConnection.ExecuteAsync(query, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar el libro: {ex.Message}");
            }
        }

        public async Task CreateLibroAsync(int id, string nombre, string titulo, string isbn, bool disponible)
        {
            try
            {
                var query = "INSERT INTO BookTable (ID, Nombre_Autor, Titulo, ISBN, Disponible) " +
                            "VALUES (@ID, @Nombre, @Titulo, @ISBN, @Disponible)";
                await _sqlConnection.ExecuteAsync(query, new { ID = id, Nombre = nombre, Titulo = titulo, ISBN = isbn, Disponible = disponible });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al crear el libro: {ex.Message}");
            }
        }

        public async Task UpdateLibroAsync(int id, string nombre, string titulo, string isbn, bool disponible)
        {
            try
            {
                var query = "UPDATE BookTable " +
                            "SET Nombre_Autor = @Nombre, Titulo = @Titulo, ISBN = @ISBN, Disponible = @Disponible " +
                            "WHERE ID = @ID";
                await _sqlConnection.ExecuteAsync(query, new { ID = id, Nombre = nombre, Titulo = titulo, ISBN = isbn, Disponible = disponible });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al actualizar el libro: {ex.Message}");
            }
        }



       

    }


}
