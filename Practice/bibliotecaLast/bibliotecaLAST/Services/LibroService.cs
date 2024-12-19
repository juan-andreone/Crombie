using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using bibliotecaLAST.Controllers;
using static bibliotecaLAST.Services.LibroService;
using bibliotecaLAST.Models;
using bibliotecaLAST.Services.Interfaces;


namespace bibliotecaLAST.Services
{

    public class LibroService : ILibroService
    {
        private readonly SqlConnection _sqlConnection;

        public LibroService(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
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

        public async Task<Libro> GetLibroByIdAsync(int id)
        {
            try
            {
                var query = "SELECT ID, Nombre_Autor, Titulo, ISBN, Disponible FROM BookTable WHERE ID = @Id";
                var libro = await _sqlConnection.QueryFirstOrDefaultAsync<Libro>(query, new { Id = id });
                return libro; 
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al consultar el libro: {ex.Message}");
            }
        }


        public async Task<IEnumerable<Libro>> GetAllLibrosAsync()
        {
            try
            {
                var query = "SELECT * FROM BookTable";
                var libros = await _sqlConnection.QueryAsync<Libro>(query);
                return libros;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener los libros: {ex.Message}");
            }
        }



    }
}