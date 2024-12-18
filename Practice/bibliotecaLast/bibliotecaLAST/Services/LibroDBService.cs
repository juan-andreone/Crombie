using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using bibliotecaLAST.Controllers;
using static bibliotecaLAST.Services.LibroDBService;
using bibliotecaLAST.Models;


namespace bibliotecaLAST.Services
{

    public class LibroDBService : ILibroDBService
    {
        private readonly SqlConnection _sqlConnection;

        public LibroDBService(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        //// Método para verificar cuántos libros tiene un usuario prestados y si puede tomar más
        //public async Task<bool> PuedeTomarPrestadoAsync(int usuarioID, string tipoUsuario)
        //{
        //    // Límite de libros dependiendo del tipo de usuario
        //    int limiteLibros = tipoUsuario == "Estudiante" ? 3 : 5;

        //    // Consulta para obtener el número de libros prestados por el usuario
        //    var query = "SELECT COUNT(*) FROM PrestamoTable WHERE UsuarioID = @UsuarioID AND FechaDevolucion IS NULL";
        //    var librosPrestados = await _sqlConnection.ExecuteScalarAsync<int>(query, new { UsuarioID = usuarioID });

        //    // Si el usuario no ha superado el límite, puede tomar prestado el libro
        //    return librosPrestados < limiteLibros;
        //}




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



    }


}
